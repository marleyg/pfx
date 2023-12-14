using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PathfinderFxGateway.Client;

namespace PathfinderFxGateway
{
    public class Authorize(ILoggerFactory loggerFactory)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<Authorize>();
        private GatewayClient? _client;

        [Function("Authorize")]
        public HttpResponseData SetCredentials([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("SetCredentials called");
            
            var clientId = req.Query["clientId"];
            var clientSecret = req.Query["clientSecret"];
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                badResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                badResponse.WriteString("Please pass a clientId and clientSecret on the query string or in the request body");
                return badResponse;
            }
            var config = new GatewayConfig
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                HostUrl = Environment.GetEnvironmentVariable("HostUrl")!,
                AuthUrl = Environment.GetEnvironmentVariable("AuthUrl")!
            };

            _client = new GatewayClient(new LoggerFactory(), config);
            var response = req.CreateResponse(HttpStatusCode.Accepted);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Credentials set");
            return response;
        }
    }
}
