using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Validation.AspNetCore;
using PathfinderFx.Config;
using PathfinderFx.Helper;
using PathfinderFx.Model;

namespace PathfinderFx.Controllers;

/// <summary>
/// WBCSD Product Footprint API v2
/// </summary>
[ApiController]
[Route("/2/")]
[Produces("application/json")]
public class ProductFootprintController : Controller
{

    private static readonly ProductFootprints ProductFootprints = LoadFootprints();
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly ILogger<ProductFootprintController> _logger;

    /// <summary>
    /// WBCSD Product Footprint API v2
    /// </summary>
    /// <param name="applicationManager"></param>
    /// <param name="logger"></param>
    public ProductFootprintController(IOpenIddictApplicationManager applicationManager,
        ILogger<ProductFootprintController> logger)
    {
        _applicationManager = applicationManager;
        _logger = logger;
    }

    private static ProductFootprints LoadFootprints()
    {
        //get a random number that is greater than 5 but less than 15
        var numFootprints = new Random().Next(5, 15);
        var footprints = new List<ProductFootprint>();
        var companyName = DataGenHelper.GenerateRandomCompanyName();
        var companyId = DataGenHelper.GenerateRandomCompanyId();
        for (var i = 0; i < numFootprints; i++)
        {
            //if i is the last iteration, add a footprint with all optional fields
            footprints.Add(i == numFootprints - 1
                ? SampleDataCreator.GetProductFootprint(companyName, companyId, true)
                : SampleDataCreator.GetProductFootprint(companyName, companyId));
        }

        return new ProductFootprints
        {
            Data = footprints
        };
    }

    /// <summary>
    /// Retrieves available footprints for the authenticated user. You can set a limit to the number of footprints returned and filter the results by product name.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="filter"></param>
    /// <param name="offset">optional if a paginated call</param>
    /// <returns>ProductFootprints</returns>
    /// <remarks>
    /// Get footprints with CPC code "3342":
    ///     $filter=productCategoryCpc eq '3342'
    /// 
    /// Get footprints scoped for country:
    ///     $filter=pcf/geographyCountry eq 'DE'
    /// 
    /// Get footprints for 2023 reporting period:
    ///     $filter=(pcf/reportingPeriodStart ge '2023-01-01T00:00:00.000Z') and (pcf/reportingPeriodStart lt '2024-01-01T00:00:00.000Z') and (pcf/reportingPeriodEnd ge '2023-01-01T00:00:00.000Z') and (pcf/reportingPeriodEnd lt '2024-01-01T00:00:00.000Z')
    /// 
    /// Get footprints for a specific product:
    ///     $filter=productIds/any(productId:(productId eq 'urn:...'))
    /// </remarks>
    [HttpGet("footprints")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ListFootprints(int limit = 0, string filter = "", int offset = 0)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = await _applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
        
        if (application == null)
        {
            return Forbid();
        }
        
        var appDisplayName = (OpenIddictEntityFrameworkCoreApplication) application;
        
        //check to make sure that the appDisplayName == _config.HostOrganizationName, all other requests are forbidden
        if (appDisplayName.Permissions == null)
        {
            return Forbid();
        }

        if (!appDisplayName.Permissions.Contains("api"))
        {
            return Forbid();
        }
        
        _logger.LogInformation("Getting footprints for user: {User} from application: {Application}", authUser,
            application);

        if (!string.IsNullOrEmpty(filter))
            _logger.LogInformation("Filtering footprints is not implemented");

        //if the offset is greater than the number of footprints in _productFootprints, return a 400
        if (offset > ProductFootprints.Data.Count)
        {
            _logger.LogInformation(
                "Offset is greater than the number of footprints, offset: {Offset}, number of footprints: {NumberOfFootprints}",
                offset, ProductFootprints.Data.Count);
            return BadRequest(new SimpleErrorMessage("Bad request", "BadRequest"));
        }

        //if the rel next header is not empty, the limit is the value of the limit query parameter
        if (offset > 0 && limit > 0)
        {
            _logger.LogInformation("Paging is enabled, continuing from offset: {Offset}", offset);

            var retVal = new ProductFootprints();
            //check to make sure that the nextItemUp is not greater than the number of footprints in _productFootprints
            if (offset < ProductFootprints.Data.Count)
            {
                //if the nextItemUp is less than the number of footprints in _productFootprints, return the footprints starting with the next item up to the limit
                retVal.Data = ProductFootprints.Data.Skip(offset).Take(limit).ToList();

                //if there are more footprints remaining update the NextItem header
                if (offset + limit < ProductFootprints.Data.Count)
                {
                    _logger.LogInformation(
                        "More footprints remaining, updating paging, offset: {Offset}, limit: {Limit}", offset + limit,
                        limit);
                    var host = HttpContext.Request.Host;
                    HttpContext.Response.Headers.Append("Link",
                        $"<https://{host}/2/footprints?limit={limit}&offset={offset + limit}>; rel=\"next\"");
                }
                else
                {
                    _logger.LogInformation("No more footprints remaining, ending paging");
                    //if there are no more footprints remaining, remove the Link header
                    HttpContext.Response.Headers.Remove("Link");
                }
            }
            else
            {
                //if the nextItemUp is greater than the number of footprints in _productFootprints, return the footprints starting with the next item up to the limit
                retVal.Data = ProductFootprints.Data.Skip(offset).Take(limit).ToList();
            }

            return Ok(retVal);

        }

        //if the limit is > 0 and there is no offset and the number of footprints is greater than the limit, enable paging
        if (limit > 0 && offset == 0 && ProductFootprints.Data.Count > limit)
        {
            _logger.LogInformation("Paging is enabled");

            //get the host from the HttpContext.Request object
            var host = HttpContext.Request.Host;

            //set nextItemUp to the count of footprints - the limit + 1
            var nextOffset = limit.ToString();

            _logger.LogInformation("Paging offset: {NextOffset}", nextOffset);
            //need to add a Link header to the response
            HttpContext.Response.Headers.Append("Link",
                $"<https://{host}/2/footprints?limit={limit}&offset={nextOffset}>; rel=\"next\"");

            var retVal = new ProductFootprints
            {
                Data = ProductFootprints.Data.Take(limit).ToList()
            };
            return Ok(retVal);
        }

        _logger.LogInformation("Paging is disabled");

        return Ok(ProductFootprints);
    }

    /// <summary>
    /// Retrieves a specific footprint by id.
    /// </summary>
    /// <param name="id">UUID/GUID</param>
    /// <returns>ProductFootprints with a matching Product Footprint if found</returns>
    [HttpGet("footprints/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetFootprint(string id)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = await _applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
        
        if (application == null)
        {
            return Forbid();
        }
        
        var appDisplayName = (OpenIddictEntityFrameworkCoreApplication) application;
        
        //check to make sure that the appDisplayName == _config.HostOrganizationName, all other requests are forbidden
        if (appDisplayName.Permissions == null)
        {
            return Forbid();
        }

        if (!appDisplayName.Permissions.Contains("api"))
        {
            return Forbid();
        }
        
        //test to see if the id is a valid Guid
        if (!Guid.TryParse(id, out _))
        {
            _logger.LogInformation("Invalid id: {Id}", id);
            return BadRequest(new SimpleErrorMessage("Bad request", "BadRequest"));
        }

        _logger.LogInformation("Getting footprint, id: {Id} for user: {User} from application: {Application}", id,
            authUser, application);

        var matchFp = ProductFootprints.Data.FirstOrDefault(x => x.Id == id);

        if (matchFp != null)
        {
            var footprints = new ProductFootprints
            {
                Data = [matchFp]
            };
            return Ok(footprints);
        }

        _logger.LogInformation("Footprint not found, id: {Id}", id);
        return NotFound(new SimpleErrorMessage("The specified footprint does not exist", "NoSuchFootprint"));
    }

    /// <summary>
    /// Establish an event one time subscription for the authenticated user to receive notifications when a footprint is created, updated, or deleted.
    /// </summary>
    /// <remarks>Currently not implemented.</remarks>
    /// <param name="actionEvent"></param>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("events")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmptyResult))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(NotImplementedResult))]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    //[Consumes("Content-Type: application/cloudevents+json; charset=UTF-8")]
    public Task<IActionResult> ActionEvent(PfRequestEvent actionEvent)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = _applicationManager.FindByClientIdAsync(authUser ?? string.Empty);

        _logger.LogInformation("Action Event: {EventBody} for user: {User} from application: {Application}",
            "actionEvent passed in", authUser, application);
        return Task.FromResult<IActionResult>(new NotImplementedResult(
            new SimpleErrorMessage(
                "The specified Action or header you provided implies functionality that is not implemented",
                "NotImplemented").ToJson()));
    }
}