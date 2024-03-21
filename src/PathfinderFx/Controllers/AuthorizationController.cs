using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using PathfinderFx.Config;
using PathfinderFx.Helper;

namespace PathfinderFx.Controllers;

public class AuthorizationController(
    IOpenIddictApplicationManager applicationManager,
    IOpenIddictScopeManager scopeManager)
    : Controller
{
    /// <summary>
    /// Authenticates the client application and returns an access token.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="ForbidResult"></exception>
    [HttpPost("~/2/auth/token"), IgnoreAntiforgeryToken, Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status501NotImplemented)]
    public async Task<IActionResult> Authenticate()
    {
        var request = HttpContext.GetOpenIddictServerRequest();
        if (!request!.IsClientCredentialsGrantType())
            return new NotImplementedResult(request!.GrantType!);
        
        // Note: the client credentials are automatically validated by OpenIddict:
        // if client_id or client_secret are invalid, this action won't be invoked.

        var application = await applicationManager.FindByClientIdAsync(request!.ClientId!);
        if (application == null)
        {
            return Forbid(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties(new Dictionary<string, string?>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidClient,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The client application was not found in the directory."
                }));
        }

        // Create the claims-based identity that will be used by OpenIddict to generate tokens.
        var identity = new ClaimsIdentity(
            authenticationType: TokenValidationParameters.DefaultAuthenticationType,
            nameType: OpenIddictConstants.Claims.Name,
            roleType: OpenIddictConstants.Claims.Role);

        // Add the claims that will be persisted in the tokens (use the client_id as the subject identifier).
        identity.SetClaim(OpenIddictConstants.Claims.Subject, await applicationManager.GetClientIdAsync(application));
        identity.SetClaim(OpenIddictConstants.Claims.Name, await applicationManager.GetDisplayNameAsync(application));

        // Note: In the original OAuth 2.0 specification, the client credentials grant
        // doesn't return an identity token, which is an OpenID Connect concept.
        //
        // As a non-standardized extension, OpenIddict allows returning an id_token
        // to convey information about the client application when the "openid" scope
        // is granted (i.e specified when calling principal.SetScopes()). When the "openid"
        // scope is not explicitly set, no identity token is returned to the client application.

        // Set the list of scopes granted to the client application in access_token.
        identity.SetScopes(request.GetScopes());
        identity.SetResources(await scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
        identity.SetDestinations(GetDestinations);

        return SignIn(new ClaimsPrincipal(identity), OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

    }
    

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        // Note: by default, claims are NOT automatically included in the access and identity tokens.
        // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
        // whether they should be included in access tokens, in identity tokens or in both.

        return claim.Type switch
        {
            OpenIddictConstants.Claims.Name or 
            OpenIddictConstants.Claims.Subject 
                => new[] { OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken },

            _ => new[] { OpenIddictConstants.Destinations.AccessToken },
        };
    }
}