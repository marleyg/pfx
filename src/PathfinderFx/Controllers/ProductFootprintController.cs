using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using PathfinderFx.Model;

namespace PathfinderFx.Controllers;

//[Authorize]
[ApiController]
[Route("/2/")]
[Produces("application/json")]
public class ProductFootprintController(
    IOpenIddictApplicationManager applicationManager,
    ILogger<ProductFootprintController> logger)
    : Controller
{
    /// <summary>
    /// Retrieves available footprints for the authenticated user. You can set a limit to the number of footprints returned and filter the results by product name.
    /// </summary>
    /// <param name="limit"></param>
    /// <param name="filter"></param>
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(SecurityTokenExpiredException))]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public ProductFootprints ListFootprints(int limit = 0, string filter = "")
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
            
        logger.LogInformation("Getting footprints for user: {User} from application: {Application}", authUser, application);
        return ProductFootprints.FromJson(System.IO.File.ReadAllText("Data/pfsv2.json"));
    }

    /// <summary>
    /// Retrieves a specific footprint by id.
    /// </summary>
    /// <param name="id">UUID/GUID</param>
    /// <returns>ProductFootprint</returns>
    [HttpGet("footprints/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFootprints))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public ProductFootprints GetFootprint(string id)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = applicationManager.FindByClientIdAsync(authUser ?? string.Empty);
            
        logger.LogInformation("Getting footprint, id: {Id} for user: {User} from application: {Application}", id, authUser, application);
        return ProductFootprints.FromJson(System.IO.File.ReadAllText("Data/pfv2.json"));
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
    public void ActionEvent(PfRequestEvent actionEvent)
    {
        var authUser = User.FindFirst(OpenIddictConstants.Claims.Subject)?.Value;
        var application = applicationManager.FindByClientIdAsync(authUser ?? string.Empty);

        logger.LogInformation("Action Event: {EventBody} for user: {User} from application: {Application}", "actionEvent passed in", authUser, application);
        throw new NotImplementedException();
    }
}