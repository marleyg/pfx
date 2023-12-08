using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using PathfinderFx.Model;

namespace PathfinderFx.Controllers;

//[Authorize]
[ApiController]
[Route("/2/")]
[Produces("application/json")]
public class ProductFootprintController
    : Controller
{
    
    private static readonly ProductFootprints ProductFootprints = LoadFootprints();
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly ILogger<ProductFootprintController> _logger;

    public ProductFootprintController(
        IOpenIddictApplicationManager applicationManager,
        ILogger<ProductFootprintController> logger)
    {
        _applicationManager = applicationManager;
        _logger = logger;
    }
    
    private static ProductFootprints LoadFootprints()
    {
        return ProductFootprints.FromJson(System.IO.File.ReadAllText("Data/pfsv2.json"));
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
    [HttpGet ("footprints")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public Task<IActionResult> ListFootprints(int limit = 0, string filter = "", int offset = 0)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = _applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
            
        _logger.LogInformation("Getting footprints for user: {User} from application: {Application}", authUser, application);
        
        if (!string.IsNullOrEmpty(filter))
            _logger.LogInformation("Filtering footprints is not implemented");
        
        //if the offset is greater than the number of footprints in _productFootprints, return a 400
        if (offset > ProductFootprints.Data.Count)
        {
            _logger.LogInformation("Offset is greater than the number of footprints, offset: {Offset}, number of footprints: {NumberOfFootprints}", offset, ProductFootprints.Data.Count);
            return Task.FromResult<IActionResult>(BadRequest(new SimpleErrorMessage("Bad request", "BadRequest")));
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
                    _logger.LogInformation("More footprints remaining, updating paging, offset: {Offset}, limit: {Limit}", offset + limit, limit);
                    var host = HttpContext.Request.Host;
                    HttpContext.Response.Headers.Append("Link", $"<https://{host}/2/footprints?limit={limit}&offset={offset + limit}>; rel=\"next\"");
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
            return Task.FromResult<IActionResult>(Ok(retVal));
            
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
            HttpContext.Response.Headers.Append("Link", $"<https://{host}/2/footprints?limit={limit}&offset={nextOffset}>; rel=\"next\"");
            
            var retVal = new ProductFootprints
            {
                Data = ProductFootprints.Data.Take(limit).ToList()
            };
            return Task.FromResult<IActionResult>(Ok(retVal));
        }

        _logger.LogInformation("Paging is disabled");
        
        return Task.FromResult<IActionResult>(Ok(ProductFootprints));
    }

    /// <summary>
    /// Retrieves a specific footprint by id.
    /// </summary>
    /// <param name="id">UUID/GUID</param>
    /// <returns>ProductFootprint</returns>
    [HttpGet("footprints/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public Task<IActionResult> GetFootprint(string id)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = _applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
        
        //test to see if the id is a valid Guid
        if (!Guid.TryParse(id, out _))
        {
            _logger.LogInformation("Invalid id: {Id}", id);
            return Task.FromResult<IActionResult>(BadRequest(new SimpleErrorMessage("Bad request", "BadRequest")));
        }
            
        _logger.LogInformation("Getting footprint, id: {Id} for user: {User} from application: {Application}", id, authUser, application);
        
        var fp = ProductFootprints.Data.Where(x => x.Id == new Guid(id)).ToList();
        if (fp.Count == 0)
        {
            _logger.LogInformation("Footprint not found, id: {Id}", id);
            return Task.FromResult<IActionResult>(NotFound(new SimpleErrorMessage("The specified footprint does not exist", "NoSuchFootprint")));
        }
        var retVal = new ProductFootprints
        {
            Data = fp
        };
        return Task.FromResult<IActionResult>(Ok(retVal));
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

        _logger.LogInformation("Action Event: {EventBody} for user: {User} from application: {Application}", "actionEvent passed in", authUser, application);
        return Task.FromResult<IActionResult>(new NotImplementedResult(
            new SimpleErrorMessage("The specified Action or header you provided implies functionality that is not implemented", "NotImplemented").ToJson()));
    }
}