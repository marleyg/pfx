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

    public string AddOrUpdateProductFootprint(ProductFootprintEntityCollection dataversePfCollection)
    {
        _logger.LogInformation("AddOrUpdateProductFootprint called with dataversePf: {ProductFootprintId}", 
            dataversePfCollection.Msdyn_SustainabilityProduct.Msdyn_SustainabilityProductId);
        var retVal = new StringBuilder();
        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductIdentifier);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductIdentifier:" + dataversePfCollection.Msdyn_SustainabilityProductIdentifier.Id + " created/updated");
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Msdyn_SustainabilityProductIdentifier");
            retVal.Append("Error in Msdyn_SustainabilityProductIdentifier:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProduct);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProduct:" + dataversePfCollection.Msdyn_SustainabilityProduct.Msdyn_Name + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in SustainabilityProduct");
            retVal.Append("Error in Msdyn_SustainabilityProduct:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductFootprint);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductFootprint:" + dataversePfCollection.Msdyn_SustainabilityProductFootprint.Msdyn_SustainabilityProductFootprintId + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in SustainabilityProductFootprint");
            retVal.Append("Error in Msdyn_SustainabilityProductFootprint:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint);
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductCarbonFootprint:" + dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint.Msdyn_SustainabilityProductCarbonFootprintId + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in SustainabilityProductCarbonFootprint");
            retVal.Append("Error in Msdyn_SustainabilityProductCarbonFootprint:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance);
            _context.SaveChanges();
            retVal.Append("Msdyn_ProductCarbonFootprintAssurance:" + dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance.Msdyn_ProductCarbonFootprintAssuranceId + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductCarbonFootprintAssurance");
            retVal.Append("Error in Msdyn_ProductCarbonFootprintAssurance:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            foreach (var rule in dataversePfCollection.Msdyn_ProductOrSectorSpecificRule)
            {
                _context.AddObject(rule);
                _context.SaveChanges();
                retVal.Append("Msdyn_ProductOrSectorSpecificRule:" + rule.Msdyn_ProductOrSectorSpecificRuleId + " created/updated");
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductOrSectorSpecificRule");
            retVal.Append("Error in Msdyn_ProductOrSectorSpecificRule:" + e);
            retVal.Append(Environment.NewLine);
        }

        try
        {
            _context.AddObject(dataversePfCollection.Msdyn_ProductFootprintRuleMapping);
            _context.SaveChanges();
            retVal.Append("Msdyn_ProductFootprintRuleMapping:" + dataversePfCollection.Msdyn_ProductFootprintRuleMapping.Msdyn_ProductFootprintRuleMappingId + " created/updated");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductFootprintRuleMapping");
            retVal.Append("Error in Msdyn_ProductFootprintRuleMapping:" + e);
            retVal.Append(Environment.NewLine);
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