using System.Text;
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

    public string AddOrUpdateProductFootprint(Msdyn_SustainabilityProductFootprint dataversePf,
        Msdyn_SustainabilityProductCarbonFootprint dataversePcf)
    {
        _logger.LogInformation("AddOrUpdateProductFootprint called with dataversePf: {ProductFootprintId}", 
            dataversePf.Msdyn_SustainabilityProductFootprintId);
        var retVal = new StringBuilder();
        try
        {
            _context.AddObject(dataversePf);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductFootprint:" + dataversePf.Id + " created/updated; ");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in AddOrUpdateProductFootprint");
            return e.Message;
        }

        try
        {
            _context.AddObject(dataversePcf);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductCarbonFootprint:" + dataversePcf.Id + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in AddOrUpdateProductCarbonFootprint");
            return e.Message;
        }

        return retVal.ToString();
    }

    public Msdyn_SustainabilityProductFootprint? GetProductFootprintByOriginCorrelationId(string? toString)
    {
        _logger.LogInformation("GetProductFootprintByOriginCorrelationId called with correlationId: {CorrelationId}", 
            toString);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductFootprintSet
                where pf.Msdyn_OriginCorrelationId == toString
                select pf;
            return query.FirstOrDefault();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetProductFootprintByOriginCorrelationId");
            return null;
        }
    }

    public Msdyn_SustainabilityProductCarbonFootprint? GetProductCarbonFootprintByOriginCorrelationId(string? toString)
    {
        _logger.LogInformation("GetProductCarbonFootprintByOriginCorrelationId called with correlationId: {CorrelationId}", 
            toString);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductCarbonFootprintSet
                where pf.Msdyn_OriginCorrelationId == toString
                select pf;
            return query.FirstOrDefault();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetProductCarbonFootprintByOriginCorrelationId");
            return null;
        }
    }
}