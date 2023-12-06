using OpenIddict.Abstractions;
using OpenIddict.Server;
using PathfinderFx.Model;

namespace PathfinderFx.Handlers;

public class PfxCustomHandlers
{
    public class PfxApplyTokenResponseHandler : IOpenIddictServerHandler<OpenIddictServerEvents.ApplyTokenResponseContext>
    {
        public ValueTask HandleAsync(OpenIddictServerEvents.ApplyTokenResponseContext context)
        {
            var response = context.Response;
            if (string.Equals(response.Error, OpenIddictConstants.Errors.InvalidClient, StringComparison.Ordinal))
            {
                response.ErrorDescription =
                    new SimpleErrorMessage("Authentication failed", "invalid_client").ToJson();
            }

            return default;
        }
    }
    
    public class PfxApplyVerificationResponseHandler: IOpenIddictServerHandler<OpenIddictServerEvents.ApplyVerificationResponseContext>
    {
        public ValueTask HandleAsync(OpenIddictServerEvents.ApplyVerificationResponseContext context)
        {
            var response = context.Response;
            if (string.Equals(response.Error, OpenIddictConstants.Errors.AccessDenied, StringComparison.Ordinal))
            {
                response.ErrorDescription =
                    new SimpleErrorMessage("Access denied", "AccessDenied").ToJson();
            }

            if (string.Equals(response.Error, OpenIddictConstants.Errors.ExpiredToken))
            {
                response.ErrorDescription =
                    new SimpleErrorMessage("The specified access token has expired", "TokenExpired").ToJson();
            }
            
            if (string.Equals(response.Error, OpenIddictConstants.Errors.InvalidToken))
            {
                response.ErrorDescription =
                    new SimpleErrorMessage("The specified access token is invalid", "TokenInvalid").ToJson();
            }

            return default;
        }
    }
    
}