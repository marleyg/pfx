using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace PathfinderFxGateway
{
    public class Gateway
    {
        private readonly ILogger _logger;

        public Gateway(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Gateway>();
        }

        [Function("Authorize")]
        public HttpResponseData Authorize([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            //initialize the PathfinderFx client with the client_id and client_secret passed in the req
            var clientId = req["client_id"];

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
