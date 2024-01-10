using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Model;

namespace PathfinderFxGateway
{
    public class Configuration(ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<Configuration>();
        internal static IPathfinderConfig PathfinderConfig = new PathfinderConfig();
        internal static IDataverseConfig DataverseConfig = new DataverseConfig();

        [Function("SetGatewayConfiguration")]
        public async Task<HttpResponseData> SetGatewayConfiguration([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("SetConfiguration called");

            try
            {
                var config = await System.Text.Json.JsonSerializer.DeserializeAsync<PathfinderConfig>(req.Body);
                if (config == null)
                {
                    PathfinderConfig.AuthUrl = Environment.GetEnvironmentVariable("AuthUrl");
                    PathfinderConfig.ClientId = Environment.GetEnvironmentVariable("ClientId");
                    PathfinderConfig.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret");
                    PathfinderConfig.HostUrl = Environment.GetEnvironmentVariable("HostUrl");
                }
                else
                {
                    PathfinderConfig.AuthUrl = config.AuthUrl;
                    PathfinderConfig.ClientId = config.ClientId;
                    PathfinderConfig.ClientSecret = config.ClientSecret;
                    PathfinderConfig.HostUrl = config.HostUrl;
                    
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
            await response.WriteStringAsync("Pathfinder Gateway Configuration set");
            return response;
        }
        
        [Function("SetDataverseConfiguration")]
        public async Task<HttpResponseData> SetDataverseConfiguration([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("SetConfiguration called");

            try
            {
                var config = await System.Text.Json.JsonSerializer.DeserializeAsync<DataverseConfig>(req.Body);
                if (config == null)
                {
                    DataverseConfig.Password = Environment.GetEnvironmentVariable("Password");
                    DataverseConfig.Url = Environment.GetEnvironmentVariable("Url");
                    DataverseConfig.UserName = Environment.GetEnvironmentVariable("UserName");
                }
                else
                {
                    DataverseConfig.Password = config.Password;
                    DataverseConfig.Url = config.Url;
                    DataverseConfig.UserName = config.UserName;
                    
                    Environment.SetEnvironmentVariable("ConnectionString", config.ConnectionString);
                    Environment.SetEnvironmentVariable("Password", config.Password);
                    Environment.SetEnvironmentVariable("Url", config.Url);
                    Environment.SetEnvironmentVariable("UserName", config.UserName);
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
            await response.WriteStringAsync("Dataverse Configuration set");
            return response;
        }
    }
}
