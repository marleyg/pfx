using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace PathfinderFxGateway.Client
{
    public class GatewayClientBase
    {
        
    private readonly ILogger _logger;
    private readonly IGatewayConfig _config;
    private BearerToken _bearer;

    /// <summary>
    ///     Constructor for GatewayClientBase, for use with type specific dependency injection in .NET Core
    /// </summary>
    public GatewayClientBase(ILoggerFactory loggerFactory, IGatewayConfig config)
    {
        _logger = loggerFactory.CreateLogger<GatewayClientBase>();
        _config = config;
    }

    protected string BaseUrl { get; set; }

    internal async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        await GetBearerToken(_config.clientId, _config.clientSecret);
        var requestMessage = new HttpRequestMessage();
        //the bearer token from Evident is called a Token, not AccessToken
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearer?.Token);

        return requestMessage;
    }

    #region authentication

    private async Task GetBearerToken(string clientId, string clientSecret)
    {
        _logger.LogInformation("GetBearerToken, checking bearer token from {Name}",
            clientId);
        try
        {
            //if bearer is null or bearer is not from today, get a new bearer
            if (_bearer == null || _bearer.ReceivedIn.Date != DateTime.Now.Date)
            {
                await NewBearer();
                return;
            }

            //check to see if the bearer has expired
            if (_bearer.ReceivedIn.TimeOfDay.Add(TimeSpan.FromMinutes(_bearer.ExpiresIn)) < DateTime.Now.TimeOfDay)
            {
                await NewBearer();
                return;
            }

            async Task NewBearer()
            {
                _bearer = BearerToken.FromJson(await GetAuthToken() ?? string.Empty);
                _bearer.ReceivedIn = DateTime.Now;
                _bearer.RefreshIn = 60;
                _logger.LogInformation("BearerToken Refreshed");
            }

            _logger.LogInformation("BearerToken Refreshing after {Minutes} minutes", _bearer.RefreshIn);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting bearer token");
            throw;
        }
    }

    private async Task<string> GetAuthToken()
    {
        _logger.LogInformation("Getting Authorization Token from {Name}", BaseUrl);
        
        var options =
            new RestClientOptions(_config?.HostUrl ?? throw new InvalidOperationException())
            {
                ThrowOnAnyError = true,
                MaxTimeout = 600000
            };
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");

        var payload = "{\"email\": \"" + email + "\", \"token\": \"" + apiToken + "\"}";
        request.AddParameter("application/json", payload, ParameterType.RequestBody);
        RestResponse response;
        try
        {
            response = await client.PostAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogError("GetAuthToken failed to {Service}, exception message {Message}",
                _config?.AuthApiUri, e.Message);
            throw;
        }

        return response.Content;
    }

    #endregion
    
    }

    public interface IGatewayConfig
    {
        [JsonProperty("host_url")]
        string HostUrl { get; set; }
        
        [JsonProperty("client_id")]
        string clientId { get; set; }
        
        [JsonProperty("client_secret")]
        string clientSecret { get; set; }
    }
    public class GatewayConfig : IGatewayConfig
    {
        public string HostUrl { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
    }

    internal class BearerToken
    {
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ReceivedIn { get; set; }
        public int RefreshIn { get; set; }

        public static BearerToken FromJson(string json) => JsonSerializer.Deserialize<BearerToken>(json);
    }
}