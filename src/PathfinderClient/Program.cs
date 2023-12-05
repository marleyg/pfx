using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using OpenIddict.Client;

var services = new ServiceCollection();

//use the appsettings.json file to configure the client
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
var clientConfig = new ClientConfig
{
    Host = config["Host"]!
};

services.AddOpenIddict()

 // Register the OpenIddict client components.
    .AddClient(options =>
    {
        // Allow grant_type=password and grant_type=refresh_token to be negotiated.
        options.AllowPasswordFlow()
               .AllowRefreshTokenFlow();

        // Disable token storage, which is not necessary for non-interactive flows like
        // grant_type=password, grant_type=client_credentials or grant_type=refresh_token.
        options.DisableTokenStorage();

        // Register the System.Net.Http integration and use the identity of the current
        // assembly as a more specific user agent, which can be useful when dealing with
        // providers that use the user agent as a way to throttle requests (e.g Reddit).
        options.UseSystemNetHttp()
               .SetProductInformation(typeof(Program).Assembly);

        // Add a client registration without a client identifier/secret attached.
        options.AddRegistration(new OpenIddictClientRegistration
        {
            Issuer = new Uri(clientConfig.Host, UriKind.Absolute)
        });
    });

await using var provider = services.BuildServiceProvider();

const string email = "bob@sample.com", password = "}s>EWG@f4g;_v7nB";

await CreateAccountAsync(provider, email, password, clientConfig);

var tokens = await GetTokensAsync(provider, email, password);
Console.WriteLine("Initial access token: {0}", tokens.AccessToken);
Console.WriteLine();
Console.WriteLine("Initial refresh token: {0}", tokens.RefreshToken);

/*
Console.WriteLine();
Console.WriteLine();

tokens = await RefreshTokensAsync(provider, tokens.RefreshToken);
Console.WriteLine("New access token: {0}", tokens.AccessToken);
Console.WriteLine();
Console.WriteLine("New refresh token: {0}", tokens.RefreshToken);
Console.WriteLine();
*/
var footprints = await GetFootprintsAsync(provider, tokens.AccessToken, clientConfig);
Console.WriteLine("Footprints: {0}", footprints);
Console.ReadLine();
return;

static async Task CreateAccountAsync(IServiceProvider provider, string email, string password, ClientConfig clientConfig)
{
    using var client = provider.GetRequiredService<HttpClient>();
    var response = await client.PostAsJsonAsync(clientConfig.Host + "/Account/Register", new { email, password });

    // Ignore 409 responses, as they indicate that the account already exists.
    if (response.StatusCode == HttpStatusCode.Conflict)
    {
        return;
    }

    response.EnsureSuccessStatusCode();
}

static async Task<(string AccessToken, string RefreshToken)> GetTokensAsync(IServiceProvider provider, string email, string password)
{
    var service = provider.GetRequiredService<OpenIddictClientService>();

    // Note: the "offline_access" scope must be requested and granted to receive a refresh token.
    var result = await service.AuthenticateWithPasswordAsync(new OpenIddictClientModels.PasswordAuthenticationRequest
    {
        Username = email,
        Password = password,
        Scopes = [OpenIddictConstants.Scopes.OfflineAccess]
    });

    return (result.AccessToken, result.RefreshToken);
}

static async Task<(string AccessToken, string RefreshToken)> RefreshTokensAsync(IServiceProvider provider, string token)
{
    var service = provider.GetRequiredService<OpenIddictClientService>();

    var result = await service.AuthenticateWithRefreshTokenAsync(new OpenIddictClientModels.RefreshTokenAuthenticationRequest
    {
        RefreshToken = token
    });

    return (result.AccessToken, result.RefreshToken);
}

static async Task<string> GetFootprintsAsync(IServiceProvider provider, string token, ClientConfig clientConfig)
{
    using var client = provider.GetRequiredService<HttpClient>();
    using var request = new HttpRequestMessage(HttpMethod.Get, clientConfig.Host + "/2/footprints");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    using var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
}


internal class ClientConfig
{
    public string Host { get; init; } = string.Empty;
}