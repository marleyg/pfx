using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PathfinderFxGateway.Client;

namespace PathfinderFxGateway
{
    public class Configuration(ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<Configuration>();
        internal static IGatewayConfig GatewayConfig = new GatewayConfig();

        [Function("Configure")]
        public async Task<HttpResponseData> SetConfiguration([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("SetConfiguration called");

            try
            {
                var config = await System.Text.Json.JsonSerializer.DeserializeAsync<GatewayConfig>(req.Body);
                if (config == null)
                {
                    GatewayConfig.AuthUrl = Environment.GetEnvironmentVariable("AuthUrl");
                    GatewayConfig.ClientId = Environment.GetEnvironmentVariable("ClientId");
                    GatewayConfig.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret");
                    GatewayConfig.HostUrl = Environment.GetEnvironmentVariable("HostUrl");
                }
                else
                {
                    GatewayConfig.AuthUrl = config.AuthUrl;
                    GatewayConfig.ClientId = config.ClientId;
                    GatewayConfig.ClientSecret = config.ClientSecret;
                    GatewayConfig.HostUrl = config.HostUrl;
                    
                    Environment.SetEnvironmentVariable("ClientId", config.ClientId);
                    Environment.SetEnvironmentVariable("ClientSecret", config.ClientSecret);
                    Environment.SetEnvironmentVariable("HostUrl", config.HostUrl);
                    Environment.SetEnvironmentVariable("AuthUrl", config.AuthUrl);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing configuration");
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                badResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                await badResponse.WriteStringAsync("Error deserializing configuration");
                return badResponse;
            }
            
            var response = req.CreateResponse(HttpStatusCode.Accepted);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await response.WriteStringAsync("Configuration set");
            return response;
        }
    }
}
