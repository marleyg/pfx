using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Clients;

namespace PathfinderFxGateway;

public class ProductFootprints(ILoggerFactory loggerFactory)
{
    [Function("GetProductFootprint")]
    public async Task<HttpResponseData> GetProductFootprint([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("GetProductFootprint");
        logger.LogInformation("GetProductFootprint called");
        
        var pfId = req.Query["pfId"];
        var gatewayClient = new GatewayClient(loggerFactory, Configuration.GatewayConfig);
        var productFootprint = await gatewayClient.FootprintAsync(pfId);
        
        var csvResult = Utils.JsonToCsv(ProductFootprint.ToJson(productFootprint), ",");
        
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync(csvResult);
        return response;
    }
}