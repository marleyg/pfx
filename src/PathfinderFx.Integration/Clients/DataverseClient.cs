using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
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

    public string AddProductFootprint(ProductFootprintEntityCollection dataversePfCollection)
    {
        _logger.LogInformation("AdProductFootprint called with dataversePf: {ProductFootprintId}", 
            dataversePfCollection.Msdyn_SustainabilityProduct?.Msdyn_SustainabilityProductId);
        
        var retVal = new StringBuilder();
        
        if (dataversePfCollection.Msdyn_SustainabilityProductIdentifier != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductIdentifier);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProductIdentifier:" +
                              dataversePfCollection.Msdyn_SustainabilityProductIdentifier.Id + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_SustainabilityProductIdentifier");
                retVal.Append("Error in Msdyn_SustainabilityProductIdentifier:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProductIdentifier: is null or not provided");
            retVal.Append(Environment.NewLine);
        }
        
        if (dataversePfCollection.Msdyn_SustainabilityProduct != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProduct);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProduct:" +
                              dataversePfCollection.Msdyn_SustainabilityProduct.Msdyn_Name + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SustainabilityProduct");
                retVal.Append("Error in Msdyn_SustainabilityProduct:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProduct: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        return retVal.ToString();
    }
    
    public string AddOrUpdateProductFootprint(ProductFootprintEntityCollection dataversePfCollection)
    {
        _logger.LogInformation("AddOrUpdateProductFootprint called with dataversePf: {ProductFootprintId}", 
            dataversePfCollection.Msdyn_SustainabilityProduct?.Msdyn_SustainabilityProductId);
        
        var retVal = new StringBuilder();
        if (dataversePfCollection.Msdyn_SustainabilityProductIdentifier != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductIdentifier);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProductIdentifier:" +
                              dataversePfCollection.Msdyn_SustainabilityProductIdentifier.Id + " created/updated");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_SustainabilityProductIdentifier");
                retVal.Append("Error in Msdyn_SustainabilityProductIdentifier:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProductIdentifier: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        if (dataversePfCollection.Msdyn_SustainabilityProduct != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProduct);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProduct:" +
                              dataversePfCollection.Msdyn_SustainabilityProduct.Msdyn_Name + " created/updated");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SustainabilityProduct");
                retVal.Append("Error in Msdyn_SustainabilityProduct:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProduct: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        if(dataversePfCollection.Msdyn_SustainabilityProductFootprint != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductFootprint);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProductFootprint:" +
                              dataversePfCollection.Msdyn_SustainabilityProductFootprint.Msdyn_SustainabilityProductFootprintId + " created/updated");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SustainabilityProductFootprint");
                retVal.Append("Error in Msdyn_SustainabilityProductFootprint:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProductFootprint: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        if (dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint);
                _context.SaveChanges();
                retVal.Append("Msdyn_SustainabilityProductCarbonFootprint:" +
                              dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint
                                  .Msdyn_SustainabilityProductCarbonFootprintId + " created/updated");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SustainabilityProductCarbonFootprint");
                retVal.Append("Error in Msdyn_SustainabilityProductCarbonFootprint:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProductCarbonFootprint: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        if (dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance);
                _context.AddRelatedObject(dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint, new Relationship("msdyn_msdyn_productcarbonfootprintassurance_msdyn_sustainabilityproductcarbonfootprint_Assurance"),
                    dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance);
                _context.SaveChanges();
                retVal.Append("Msdyn_ProductCarbonFootprintAssurance:" +
                              dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance
                                  .Msdyn_ProductCarbonFootprintAssuranceId + " created/updated");
                retVal.Append(Environment.NewLine);
           
                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in ProductCarbonFootprintAssurance");
                retVal.Append("Error in Msdyn_ProductCarbonFootprintAssurance:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_ProductCarbonFootprintAssurance: is null or not provided");
            retVal.Append(Environment.NewLine);
        }

        if(dataversePfCollection.Msdyn_ProductOrSectorSpecificRule != null)
        {
            try
            {
                foreach (var rule in dataversePfCollection.Msdyn_ProductOrSectorSpecificRule)
                {
                    _context.AddObject(rule);
                    _context.SaveChanges();
                    retVal.Append("Msdyn_ProductOrSectorSpecificRule:" + rule.Msdyn_ProductOrSectorSpecificRuleId + " created/updated");
                    retVal.Append(Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in ProductOrSectorSpecificRule");
                retVal.Append("Error in Msdyn_ProductOrSectorSpecificRule:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_ProductOrSectorSpecificRule: is null or not provided");
            retVal.Append(Environment.NewLine);
        }
        
        if(dataversePfCollection.Msdyn_ProductFootprintRuleMapping != null)
        {
            try
            {
                _context.AddObject(dataversePfCollection.Msdyn_ProductFootprintRuleMapping);
                _context.SaveChanges();
                retVal.Append("Msdyn_ProductFootprintRuleMapping:" +
                              dataversePfCollection.Msdyn_ProductFootprintRuleMapping
                                  .Msdyn_ProductFootprintRuleMappingId + " created/updated");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in ProductFootprintRuleMapping");
                retVal.Append("Error in Msdyn_ProductFootprintRuleMapping:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_ProductFootprintRuleMapping: is null or not provided");
            retVal.Append(Environment.NewLine);
        }
        
        return retVal.ToString();
    }

    public Msdyn_SustainabilityProductIdentifier? GetProductFootprintIdentifierById(string? id)
    {
        _logger.LogInformation("GetSustainabilityProductIdentifier called with Id: {Id}", 
            id);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductIdentifierSet
                where pf.Msdyn_Name == id
                select pf;
            return query.FirstOrDefault();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetProductFootprintByOriginCorrelationId");
            return null;
        }
    }

    public string CleanDataverseTables()
    {
        _logger.LogInformation("CleanDataverseTables called");
        var retVal = new StringBuilder();
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductIdentifierSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductIdentifierSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductIdentifierSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductFootprintSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductFootprintSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductFootprintSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductCarbonFootprintSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_SustainabilityProductCarbonFootprintSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductCarbonFootprintSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        
        try
        {
            var query = from pf in _context.Msdyn_ProductCarbonFootprintAssuranceSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_ProductCarbonFootprintAssuranceSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductCarbonFootprintAssuranceSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        
        try
        {
            var query = from pf in _context.Msdyn_ProductOrSectorSpecificRuleSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_ProductOrSectorSpecificRuleSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductOrSectorSpecificRuleSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        
        try
        {
            var query = from pf in _context.Msdyn_ProductFootprintRuleMappingSet
                select pf;
            foreach (var pf in query)
            {
                _context.DeleteObject(pf);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_ProductFootprintRuleMappingSet cleaned");
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductFootprintRuleMappingSet:" + e);
        }
        retVal.Append(Environment.NewLine);
        
        return retVal.ToString();
        
    }
    
}