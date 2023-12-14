using System.Globalization;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace PathfinderFxGateway.Client
{
    public class GatewayClientBase
    {
        
    private readonly ILogger _logger;
    protected readonly IGatewayConfig Config;
    private BearerToken? _bearer;
    protected readonly HttpClient HttpClient;

    /// <summary>
    ///     Constructor for GatewayClientBase, for use with type specific dependency injection in .NET Core
    /// </summary>
    protected GatewayClientBase(ILoggerFactory loggerFactory, IGatewayConfig config)
    {
        _logger = loggerFactory.CreateLogger<GatewayClientBase>();
        Config = config;
        HttpClient = new HttpClient();
        BaseUrl = Config.HostUrl ?? throw new InvalidOperationException();
    }

    protected string BaseUrl { get; set; }

    internal async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
    {
        await GetBearerToken(Config.ClientId, Config.ClientSecret);
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
        _logger.LogTrace("Getting Authorization Token from {Name}", Config.HostUrl);
        var options =
            new RestClientOptions(Config.AuthUrl  ?? throw new InvalidOperationException())
            {
                ThrowOnAnyError = true,
                MaxTimeout = 600000
            };
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("client_id", Config.ClientId);
        request.AddParameter("grant_type", "client_credentials");
        request.AddParameter("client_secret", Config.ClientSecret);

        RestResponse? response;
        try
        {
            response = await client.PostAsync(request);
        }
        catch (Exception e)
        {
            _logger.LogError("GetAuthToken failed to {Service}, exception message {Message}",
                Config.HostUrl, e.Message);
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
        
        [JsonProperty("auth_url")]
        string AuthUrl { get; set; }
        
        [JsonProperty("client_id")]
        string ClientId { get; set; }
        
        [JsonProperty("client_secret")]
        string ClientSecret { get; set; }
    }
    public class GatewayConfig : IGatewayConfig
    {
        public string HostUrl { get; set; }
        public string AuthUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public partial class BearerToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        public long ExtExpiresIn { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }
        
        [JsonProperty("received_in")]
        public DateTime ReceivedIn { get; set; }
        
        [JsonProperty("refresh_in")]
        public int RefreshIn { get; set; }
        
    }

    public partial class BearerToken
    {
        public static BearerToken FromJson(string json) => JsonConvert.DeserializeObject<BearerToken>(json, TokenConverter.Settings);
    }
    internal static class TokenConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}