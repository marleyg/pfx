using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
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
        _logger.LogInformation("AddProductFootprint called with dataversePf: {ProductFootprintId}", 
            dataversePfCollection.Msdyn_SustainabilityProduct?.Msdyn_SustainabilityProductId);
        
        var retVal = new StringBuilder();
        Guid productId = default;
        if (dataversePfCollection.Msdyn_SustainabilityProduct != null)
        {
            try
            {
                productId = CreateProduct(dataversePfCollection.Msdyn_SustainabilityProduct);
                retVal.Append("Msdyn_SustainabilityProduct:" +
                              productId + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_SustainabilityProduct");
                retVal.Append("Error in Msdyn_SustainabilityProduct:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        else
        {
            retVal.Append("Msdyn_SustainabilityProductIdentifier: is null or not provided");
            retVal.Append(Environment.NewLine);
        }
        
        if (dataversePfCollection.Msdyn_SustainabilityProductIdentifier != null)
        {
            try
            {
                var identifierId = CreateProductIdentifier(dataversePfCollection.Msdyn_SustainabilityProductIdentifier, productId);
                retVal.Append("Msdyn_SustainabilityProductIdentifier:" +
                              identifierId + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SustainabilityProductIdentifier");
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
    
   
    
    public Guid CreateProduct(Msdyn_SustainabilityProduct product)
    {
        _logger.LogInformation("CreateProduct called with product: {Product}", 
            product.Msdyn_Name);
        
        var request = new CreateRequest
        {
            // Set the Target property
            Target = product
        };

        // Send the request using the IOrganizationService.Execute method
        // Cast the OrganizationResponse into a CreateResponse
        var response = (CreateResponse) _context.Execute(request);
        _logger.LogInformation("Product created with id: {Id}", 
            response.id);
        // Return the id property value
        return response.id;
    }
    
    public Guid? CreateProductIdentifier(Msdyn_SustainabilityProductIdentifier identifier, Guid productId)
    {
        _logger.LogInformation("CreateProductIdentifier called with identifier: {Identifier}", 
            identifier.Msdyn_Name);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductIdentifierSet
                where pf.Msdyn_Name == identifier.Msdyn_Name
                select pf;
            var existingIdentifier = query.FirstOrDefault();
            if (existingIdentifier != null)
            {
                _logger.LogInformation("ProductIdentifier already exists, updating the reference and returning existing id: {Id}", 
                    existingIdentifier.Msdyn_SustainabilityProductIdentifierId);
                
                existingIdentifier.Msdyn_SustainabilityProduct = new EntityReference(Msdyn_SustainabilityProduct.EntityLogicalName, productId);
                _context.UpdateObject(existingIdentifier);
                _context.SaveChanges();
                return existingIdentifier.Msdyn_SustainabilityProductIdentifierId;
            }

            _logger.LogInformation("ProductIdentifier does not exist, creating new identifier");
            identifier.Msdyn_SustainabilityProduct = new EntityReference(Msdyn_SustainabilityProduct.EntityLogicalName, productId);
            
            var request = new CreateRequest
            {
                Target = identifier
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse) _context.Execute(request);
            
            _logger.LogInformation("ProductIdentifier created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateProductIdentifier");
            return Guid.Empty;
        }
    }
    
    #region clean dataverse tables
    
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
    #endregion
}