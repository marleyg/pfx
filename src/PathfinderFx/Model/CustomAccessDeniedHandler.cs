using OpenIddict.Abstractions;
using OpenIddict.Server;

namespace PathfinderFx.Model;
public class MyApplyTokenResponseHandler : IOpenIddictServerHandler<OpenIddictServerEvents.ApplyTokenResponseContext>
{
    public ValueTask HandleAsync(OpenIddictServerEvents.ApplyTokenResponseContext context)
    {
        var response = context.Response;
        if (string.Equals(response.Error, OpenIddictConstants.Errors.InvalidGrant, StringComparison.Ordinal))
        {
            response.ErrorDescription = new SimpleErrorMessage("The username/password couple is invalid.", "invalid_grant").ToJson();
        }

        return default;
    }
}