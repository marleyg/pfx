using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using PathfinderFx.Integration.Model;
using PathfinderFx.Integration.Model.Entities;

namespace PathfinderFx.Integration.Clients;

public class DataverseClient
{
    private readonly ServiceClient _orgService; 
    private readonly ILogger _logger;
    private readonly DataverseContext _context;

    public DataverseClient(ILoggerFactory loggerFactory, IDataverseConfig config)
    {
        _logger = loggerFactory.CreateLogger<DataverseClient>();
        _logger.LogInformation("DataverseClient constructor called");
        _orgService = new ServiceClient(config.ConnectionString);
        _context = new DataverseContext(_orgService);
    }
    public string WhoAmI()
    {
        _logger.LogInformation("WhoAmI called");
        try
        {
            var response = (WhoAmIResponse)_orgService.Execute(new WhoAmIRequest());
            return response.UserId.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in WhoAmI");
            return e.Message;
        }
    }

    public string AddOrUpdateProductFootprint(Msdyn_SustainabilityProductFootprint productFootprint)
    {
        _logger.LogInformation("AddOrUpdateProductFootprint called with productFootprint: {ProductFootprintId}", 
            productFootprint.Msdyn_SustainabilityProductFootprintId);
        try
        {
            _context.AddObject(productFootprint);
            _context.SaveChanges();
            return productFootprint.Id.ToString();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in AddOrUpdateProductFootprint");
            return e.Message;
        }
    }
}