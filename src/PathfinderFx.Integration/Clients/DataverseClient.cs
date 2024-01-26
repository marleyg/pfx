using System.Text;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
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
    
    #region test authentication
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

    #endregion
    
    #region Add and Updates
    
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
                retVal.Append("Msdyn_SustainabilityProduct: " + dataversePfCollection.Msdyn_SustainabilityProduct.Msdyn_Name + " provided");
                productId = CreateProduct(dataversePfCollection.Msdyn_SustainabilityProduct);
                retVal.Append("Msdyn_SustainabilityProduct:" +
                              productId + " created or updated");
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
            retVal.Append("Msdyn_SustainabilityProduct: is null or not provided");
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

        Guid footprintId = default;
        if (dataversePfCollection.Msdyn_SustainabilityProductFootprint != null)
        {
            try
            {
                footprintId = CreateProductFootprint(dataversePfCollection.Msdyn_SustainabilityProductFootprint, productId);
                retVal.Append("Msdyn_SustainabilityProductFootprint:" +
                              footprintId + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_SustainabilityProductFootprint");
                retVal.Append("Error in Msdyn_SustainabilityProductFootprint:" + e);
                retVal.Append(Environment.NewLine);
            }
        }

        if (!string.IsNullOrEmpty(dataversePfCollection.HostName))
        {
            try
            {
                var result = CreateImportRecord(dataversePfCollection.HostName, dataversePfCollection.Msdyn_SustainabilityProduct!.Msdyn_SustainabilityProductId);
                retVal.Append("Msdyn_PathfinderFxPcFImports:" +
                              result + " created");
                retVal.Append(Environment.NewLine);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_PathfinderFxPcFImports");
                retVal.Append("Error in Msdyn_PathfinderFxPcFImports:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        
        Guid carbonFootprintId = default;
        if (dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint != null)
        {
            try
            {
                if (dataversePfCollection.Msdyn_ProductCarbonFootprintAssurance != null)
                {
                    var assuranceId = CreateProductCarbonFootprintAssurance(dataversePfCollection
                        .Msdyn_ProductCarbonFootprintAssurance);

                    dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint
                            .Msdyn_ProductCarbonFootprintAssurance =
                        new EntityReference(Msdyn_ProductCarbonFootprintAssurance.EntityLogicalName, assuranceId);
                }

                carbonFootprintId = CreateProductCarbonFootprint(dataversePfCollection.Msdyn_SustainabilityProductCarbonFootprint);
                retVal.Append("Msdyn_SustainabilityProductCarbonFootprint:" +
                              carbonFootprintId + " created");
                retVal.Append(Environment.NewLine);
                var updatePfResult = UpdateProductFootprint(footprintId, carbonFootprintId);
                retVal.Append("Msdyn_SustainabilityProductFootprint reference updated:" +
                              updatePfResult);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_SustainabilityProductCarbonFootprint");
                retVal.Append("Error in Msdyn_SustainabilityProductCarbonFootprint:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        
        if (dataversePfCollection.Msdyn_ProductOrSectorSpecificRule != null)
        {
            try
            {
                foreach (var rule in dataversePfCollection.Msdyn_ProductOrSectorSpecificRule)
                {
                    var ruleId =
                        CreateProductOrSectorSpecificRule(rule);
                    retVal.Append("Msdyn_ProductOrSectorSpecificRule:" +
                                  ruleId + " created");
                    retVal.Append(Environment.NewLine);
                    
                    var mappingResult = CreateRuleMapping(ruleId, carbonFootprintId);
                    retVal.Append("Msdyn_ProductFootprintRuleMapping:" +
                                  mappingResult + " created");
                    retVal.Append(Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Msdyn_ProductOrSectorSpecificRule");
                retVal.Append("Error in Msdyn_ProductOrSectorSpecificRule:" + e);
                retVal.Append(Environment.NewLine);
            }
        }
        
        return retVal.ToString();
    }

    private string CreateImportRecord(string hostName, Guid? sustainabilityProductId)
    {
        _logger.LogInformation("CreateImportRecord called with hostName: {HostName} and sustainabilityProductId: {SustainabilityProductId}", 
            hostName, sustainabilityProductId);
        try
        {
            var configEntry = from config in _context.Msdyn_PathfinderFxConfigurationSet
                where config.Msdyn_HostName == hostName
                select config;
            var configuration = configEntry.FirstOrDefault();
            if (configuration is { Msdyn_PathfinderFxConfigurationId: not null })
            {
                var import = new Msdyn_PathfinderFxPcFImports
                {
                    Msdyn_PFId = sustainabilityProductId.ToString() ?? throw new InvalidOperationException("sustainabilityProductId is null"),
                    New_Parent_Host = new EntityReference(Msdyn_PathfinderFxConfiguration.EntityLogicalName, (Guid)configuration.Msdyn_PathfinderFxConfigurationId)
                };
                var request = new CreateRequest
                {
                    Target = import
                };
                var response = (CreateResponse) _context.Execute(request);
                _logger.LogInformation("Import record created with id: {Id}", 
                    response.id);
                return response.id.ToString();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateImportRecord");
            return string.Empty;
        }
        return "";
    }

    public Guid CreateProduct(Msdyn_SustainabilityProduct product)
    {
        _logger.LogInformation("CreateProduct called with product: {Product}", 
            product.Msdyn_Name);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductSet
                where pf.Msdyn_Name == product.Msdyn_Name
                select pf;
            var existingIdentifier = query.FirstOrDefault();
            if (existingIdentifier != null)
            {
                _logger.LogInformation(
                    "Product already exists, updating the reference and returning existing id: {Id}",
                    existingIdentifier.Msdyn_Name);
                return new Guid(product.Msdyn_Name);
            }

            _logger.LogInformation("Product does not exist, creating new SustainabilityProduct");
            var request = new CreateRequest
            {
                // Set the Target property
                Target = product
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse)_context.Execute(request);
            _logger.LogInformation("Product created with id: {Id}",
                response.id);
            // Return the id property value
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateProduct");
            return Guid.Empty;
        }
    }
    
    public Guid? CreateProductIdentifier(Msdyn_SustainabilityProductIdentifier identifier, Guid productId)
    {
        _logger.LogInformation("CreateProductIdentifier called with productCarbonFootprint: {Identifier}", 
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

            _logger.LogInformation("ProductIdentifier does not exist, creating new productCarbonFootprint");
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
    
    public Guid CreateProductFootprint(Msdyn_SustainabilityProductFootprint productFootprint, Guid productId)
    {
        _logger.LogInformation("CreateProductFootprint called with ProductId: {Identifier}", 
            productId);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductFootprintSet
                where pf.Msdyn_Name == productFootprint.Msdyn_Name
                select pf;
            var existingProductFootprint = query.FirstOrDefault();
            if (existingProductFootprint != null)
            {
                _logger.LogInformation("ProductIdentifier already exists, updating the reference and returning existing id: {Id}", 
                    existingProductFootprint.Msdyn_Name);
                
                existingProductFootprint.Msdyn_SustainabilityProduct = new EntityReference(Msdyn_SustainabilityProduct.EntityLogicalName, productId);
                _context.UpdateObject(existingProductFootprint);
                _context.SaveChanges();
                return new Guid(existingProductFootprint.Msdyn_Name);
            }

            _logger.LogInformation("ProductFootprint does not exist, creating new ProductFootprint");
            productFootprint.Msdyn_SustainabilityProduct = new EntityReference(Msdyn_SustainabilityProduct.EntityLogicalName, productId);
            
            var request = new CreateRequest
            {
                Target = productFootprint
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse) _context.Execute(request);
            
            _logger.LogInformation("ProductFootprint created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductFootprint");
            return Guid.Empty;
        }
    }
    
    private bool UpdateProductFootprint(Guid footprintId, Guid carbonFootprintId)
    {
        _logger.LogInformation("UpdateProductFootprint called with FootprintId: {Identifier} to add entity reference to PCF: {CarbonFootprintId}", 
            footprintId, carbonFootprintId);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductFootprintSet
                where pf.Msdyn_SustainabilityProductFootprintId == footprintId
                select pf;
            var existingProductFootprint = query.FirstOrDefault();
            if (existingProductFootprint != null)
            {
                _logger.LogInformation("ProductFootprint found, updating the reference and returning existing id: {Id}", 
                    existingProductFootprint.Msdyn_Name);
                
                existingProductFootprint.Msdyn_SustainabilityProductCarbonFootprint = new EntityReference(Msdyn_SustainabilityProductCarbonFootprint.EntityLogicalName, footprintId);
                _context.UpdateObject(existingProductFootprint);
                _context.SaveChanges();
                return existingProductFootprint.Msdyn_SustainabilityProductFootprintId != null;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductFootprint");
            return false;
        }
    }
    
    public Guid CreateProductCarbonFootprint(Msdyn_SustainabilityProductCarbonFootprint productCarbonFootprint)
    {
        _logger.LogInformation("CreateProductCarbonFootprint called with ProductId: {Identifier}", 
            productCarbonFootprint.Msdyn_Name);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductCarbonFootprintSet
                where pf.Msdyn_Name == productCarbonFootprint.Msdyn_Name
                select pf;
            var existingProductCarbonFootprint = query.FirstOrDefault();
            if (existingProductCarbonFootprint != null)
            {
                _logger.LogInformation("ProductCarbonFootprint already exists, updating the reference and returning existing id: {Id}", 
                    existingProductCarbonFootprint.Msdyn_Name);
                return new Guid(existingProductCarbonFootprint.Msdyn_Name);
            }

            _logger.LogInformation("ProductFootprint does not exist, creating new ProductFootprint");
            
            var request = new CreateRequest
            {
                Target = productCarbonFootprint
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse) _context.Execute(request);
            
            _logger.LogInformation("ProductCarbonFootprint created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductCarbonFootprint");
            return Guid.Empty;
        }
    }
    
    private Guid CreateProductCarbonFootprintAssurance(Msdyn_ProductCarbonFootprintAssurance productCarbonFootprintAssurance)
    {
        _logger.LogInformation("CreateProductCarbonFootprintAssurance called with ProductId: {Identifier}", 
            productCarbonFootprintAssurance.Msdyn_Name);
        try
        {
            var query = from pf in _context.Msdyn_ProductCarbonFootprintAssuranceSet
                where pf.Msdyn_Name == productCarbonFootprintAssurance.Msdyn_Name
                select pf;
            var existingProductCarbonFootprintAssurance = query.FirstOrDefault();
            if (existingProductCarbonFootprintAssurance != null)
            {
                _logger.LogInformation("ProductCarbonFootprintAssurance already exists, updating the reference and returning existing id: {Id}", 
                    existingProductCarbonFootprintAssurance.Msdyn_Name);
                return new Guid(existingProductCarbonFootprintAssurance.Msdyn_Name);
            }

            _logger.LogInformation("ProductCarbonFootprintAssurance does not exist, creating new ProductCarbonFootprintAssurance");
            
            var request = new CreateRequest
            {
                Target = productCarbonFootprintAssurance
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse) _context.Execute(request);
            
            _logger.LogInformation("ProductCarbonFootprintAssurance created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductCarbonFootprintAssurance");
            return Guid.Empty;
        }
    }

    private Guid CreateProductOrSectorSpecificRule(Msdyn_ProductOrSectorSpecificRule productOrSectorSpecificRule)
    {
        _logger.LogInformation("CreateProductOrSectorSpecificRule called with Operator: {Identifier}", 
            productOrSectorSpecificRule.Msdyn_Operator);
        try
        {
            var query = from pf in _context.Msdyn_ProductOrSectorSpecificRuleSet
                where pf.Msdyn_Operator == productOrSectorSpecificRule.Msdyn_Operator &&
                      pf.Msdyn_RuleNames == productOrSectorSpecificRule.Msdyn_RuleNames
                select pf;
            var existingProductOrSectorSpecificRule = query.FirstOrDefault();
            if (existingProductOrSectorSpecificRule != null)
            {
                _logger.LogInformation("ProductOrSectorSpecificRule already exists, updating the reference and returning existing id: {Id}", 
                    existingProductOrSectorSpecificRule.Msdyn_ProductOrSectorSpecificRuleId);
                return existingProductOrSectorSpecificRule.Msdyn_ProductOrSectorSpecificRuleId ?? new Guid();
            }

            _logger.LogInformation("ProductOrSectorSpecificRule does not exist, creating new ProductOrSectorSpecificRule");
            
            var request = new CreateRequest
            {
                Target = productOrSectorSpecificRule
            };

            // Send the request using the IOrganizationService.Execute method
            // Cast the OrganizationResponse into a CreateResponse
            var response = (CreateResponse) _context.Execute(request);
            
            _logger.LogInformation("ProductOrSectorSpecificRule created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in ProductOrSectorSpecificRule");
            return Guid.Empty;
        }
    }
    
    private Guid CreateRuleMapping(Guid ruleId, Guid carbonFootprintId)
    {
        _logger.LogInformation("CreateRuleMapping called with ruleId: {Identifier} and carbonFootprintId: {CarbonFootprintId}",
            ruleId, carbonFootprintId);
        try
        {
            var newMapping = new Msdyn_ProductFootprintRuleMapping
            {
                Msdyn_ProductOrSectorSpecificRule =
                    new EntityReference(Msdyn_ProductOrSectorSpecificRule.EntityLogicalName, ruleId),
                Msdyn_ProductCarbonFootprint =
                    new EntityReference(Msdyn_SustainabilityProductCarbonFootprint.EntityLogicalName, carbonFootprintId)
            };
            
            var request = new CreateRequest
            {
                Target = newMapping
            };

            var response = (CreateResponse) _context.Execute(request);
            _logger.LogInformation("ProductOrSectorSpecificRule created with id: {Id}", 
                response.id);
            return response.id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CreateRuleMapping");
            return Guid.Empty;
        }
    }

    #endregion

    #region Queries

    
    public Msdyn_SustainabilityProductIdentifier? GetProductFootprintIdentifierById(Guid id)
    {
        _logger.LogInformation("GetSustainabilityProductIdentifier called with Id: {Id}", 
            id);
        try
        {
            var query = from pf in _context.Msdyn_SustainabilityProductIdentifierSet
                where pf.Msdyn_SustainabilityProductIdentifierId == id
                select pf;
            return query.FirstOrDefault();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetProductFootprintByOriginCorrelationId");
            return null;
        }
    }

    public List<Msdyn_Unit> GetUnits()
    {
        _logger.LogInformation("GetUnits called");
        try
        {
            var query = from pf in _context.Msdyn_UnitSet
                select pf;
            return query.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetUnits");
            return [];
        }
    } 
    
    public List<Msdyn_PathfinderFxConfiguration> GetPathfinderFxConfiguration()
    {
        _logger.LogInformation("GetPathfinderFxConfiguration called");
        try
        {
            var query = from pf in _context.Msdyn_PathfinderFxConfigurationSet
                select pf;
            return query.ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetPathfinderFxConfiguration");
            return [];
        }
    }

    #endregion
    
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductIdentifierSet:" + e);
            retVal.Append(Environment.NewLine);
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductSet:" + e);
            retVal.Append(Environment.NewLine);
        }
        retVal.Append(Environment.NewLine);

        try
        {
            var query = from import in _context.Msdyn_PathfinderFxPcFImportsSet
                select import;
            foreach (var import in query)
            {
                _context.DeleteObject(import);
            }
            _context.SaveChanges();
            retVal.Append("Msdyn_PathfinderFxPcFImportsSet cleaned");
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_PathfinderFxPcFImportsSet:" + e);
            retVal.Append(Environment.NewLine);
        }
        
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductFootprintSet:" + e);
            retVal.Append(Environment.NewLine);
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_SustainabilityProductCarbonFootprintSet:" + e);
            retVal.Append(Environment.NewLine);
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductCarbonFootprintAssuranceSet:" + e);
            retVal.Append(Environment.NewLine);
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductOrSectorSpecificRuleSet:" + e);
            retVal.Append(Environment.NewLine);
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
            retVal.Append(Environment.NewLine);
        }
        catch (Exception e)
        {
            retVal.Append("Error in Msdyn_ProductFootprintRuleMappingSet:" + e);
            retVal.Append(Environment.NewLine);
        }
        retVal.Append(Environment.NewLine);
        
        return retVal.ToString();
    }
    #endregion
    
    #region Setup Pathfinder Tables

    private const string ConfigurationEntityName = "msdyn_pathfinderfxconfiguration";
    private const string PcfImportEntityName = "msdyn_pathfinderfxpcfimports";

    public bool InitializePathfinderFxConfiguration()
    {
        _logger.LogInformation("InitializePathfinderFxConfiguration called");
        try
        {
            CleanOldConfigurationTable();
            CleanOldImportsTable();
            CreatePathfinderFxConfigurationTable();
            CreateHostUrlAttribute();
            CreateHostAuthUrlAttribute();
            CreateClientIdAttribute();
            CreateClientSecretAttribute();

            _logger.LogInformation("Creating PathfinderFxPcfImports");
            CreatePathfinderFxPcfImports();
            CreateRelationshipBetweenPcfImportsAndConfiguration();
            
            return true;
        }  
        catch (Exception e)
        {
            _logger.LogError(e, "Error in InitializePathfinderFxConfiguration");
            return false;
        }
    }

    private void CleanOldConfigurationTable()
    {
        //look for a table called Msdyn_PathfinderFxConfiguration and if found delete it
        try{
            var request = new DeleteEntityRequest
            {
                LogicalName = ConfigurationEntityName.ToLower()
            };
            _context.Execute(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CleanOldConfigurationTable, may not exist yet");
        }
    }
    
    private void CleanOldImportsTable()
    {
        //look for a table called Msdyn_PcfImportEntityName and if found delete it
        try{
            var request = new DeleteEntityRequest
            {
                LogicalName = PcfImportEntityName.ToLower()
            };
            _context.Execute(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in CleanOldImportsTable, may not exist yet");
        }
    }
    
    private void CreatePathfinderFxConfigurationTable()
    {
        var createConfigTableRequest = new CreateEntityRequest
        {
            Entity = new EntityMetadata
            {
                SchemaName = ConfigurationEntityName,
                DisplayName = new Label("Pathfinder Network Settings", 1033),
                DisplayCollectionName = new Label("Pathfinder Settings", 1033),
                Description = new Label("An entity to store network host addresses for exchange PCF data", 1033),
                OwnershipType = OwnershipTypes.UserOwned,
                IsActivity = false,
            },

            PrimaryAttribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_hostname",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 100,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("Host's Organization Name", 1033),
                Description = new Label("The primary attribute for the Pathfinder Host entity.", 1033)
            }
        };

        _logger.LogInformation("CreatePathfinderFxConfigurationTable called");
        _context.Execute(createConfigTableRequest);
    }

    private void CreateHostUrlAttribute()
    {
        var createHostUrlAttributeRequest = new CreateAttributeRequest
        {
            EntityName = ConfigurationEntityName,
            Attribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_hostbaseurl",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 255,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("Host URL", 1033),
                Description = new Label("Pathfinder Base Host Url", 1033)
            }
        };
        _logger.LogInformation("CreateHostUrlAttribute called");
        _context.Execute(createHostUrlAttributeRequest);
    }
    
    private void CreateHostAuthUrlAttribute()
    {
        var createHostAuthUrlAttributeRequest = new CreateAttributeRequest
        {
            EntityName = ConfigurationEntityName,
            Attribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_hostauthurl",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 255,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("Host Auth URL", 1033),
                Description = new Label("Pathfinder Host Authorization Url", 1033)
            }
        };
        _logger.LogInformation("CreateHostAuthUrlAttribute called");
        _context.Execute(createHostAuthUrlAttributeRequest);
    }
    
    private void CreateClientIdAttribute()
    {
        var createClientIdAttributeRequest = new CreateAttributeRequest
        {
            EntityName = ConfigurationEntityName,
            Attribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_clientid",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 255,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("Client Id", 1033),
                Description = new Label("Pathfinder Client Id", 1033)
            }
        };
        _logger.LogInformation("CreateClientIdAttribute called");
        _context.Execute(createClientIdAttributeRequest);
    }
    
    private void CreateClientSecretAttribute()
    {
        var createClientSecretAttributeRequest = new CreateAttributeRequest
        {
            EntityName = ConfigurationEntityName,
            Attribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_clientsecret",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = 255,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("Client Secret", 1033),
                Description = new Label("Pathfinder Client Secret", 1033)
            }
        };
        _logger.LogInformation("CreateClientSecretAttribute called");
        _context.Execute(createClientSecretAttributeRequest);
    }

    private void CreatePathfinderFxPcfImports()
    {
        var createConfigTableRequest = new CreateEntityRequest
        {
            Entity = new EntityMetadata
            {
                SchemaName = PcfImportEntityName,
                DisplayName = new Label("Pathfinder PCF Imports", 1033),
                DisplayCollectionName = new Label("Pathfinder Imports", 1033),
                Description = new Label("An entity to store a history of PCF imports from the Pathfinder Network", 1033),
                OwnershipType = OwnershipTypes.UserOwned,
                IsActivity = false,
            },

            PrimaryAttribute = new StringAttributeMetadata
            {
                SchemaName = "msdyn_pfId",
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                MaxLength = Guid.NewGuid().ToString().Length,
                FormatName = StringFormatName.Text,
                DisplayName = new Label("The Product Footprint Id", 1033),
                Description = new Label("The primary attribute for the imported PCF.", 1033)
            }
        };

        _logger.LogInformation("CreatePcfImports called");
        _context.Execute(createConfigTableRequest);
    }

    private void CreateRelationshipBetweenPcfImportsAndConfiguration()
    {
        var eligibleCreateOneToManyRelationship =
            EligibleCreateOneToManyRelationship(ConfigurationEntityName, PcfImportEntityName);

        if (!eligibleCreateOneToManyRelationship) return;
        var createOneToManyRelationshipRequest =
            new CreateOneToManyRequest
            {
                OneToManyRelationship =
                    new OneToManyRelationshipMetadata
                    {
                        ReferencedEntity = ConfigurationEntityName,
                        ReferencingEntity = PcfImportEntityName,
                        SchemaName = "new_host_pcf",
                        AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                        {
                            Behavior = AssociatedMenuBehavior.UseLabel,
                            Group = AssociatedMenuGroup.Details,
                            Label = new Label("PathfinderHost", 1033),
                            Order = 10000
                        },
                        CascadeConfiguration = new CascadeConfiguration
                        {
                            Assign = CascadeType.NoCascade,
                            Delete = CascadeType.RemoveLink,
                            Merge = CascadeType.NoCascade,
                            Reparent = CascadeType.NoCascade,
                            Share = CascadeType.NoCascade,
                            Unshare = CascadeType.NoCascade
                        }
                    },
                Lookup = new LookupAttributeMetadata
                {
                    SchemaName = "new_parent_host",
                    DisplayName = new Label("Host Lookup", 1033),
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    Description = new Label("Host Lookup", 1033)
                }
            };
        
        var createOneToManyRelationshipResponse =
            (CreateOneToManyResponse)_context.Execute(
                createOneToManyRelationshipRequest);

        var oneToManyRelationshipId =
            createOneToManyRelationshipResponse.RelationshipId;
        var oneToManyRelationshipName =
            createOneToManyRelationshipRequest.OneToManyRelationship.SchemaName;

        _logger.LogInformation(
            "The one-to-many relationship {Guid} with {SchemaName} has been created between {One} and {Two}",
            oneToManyRelationshipId, oneToManyRelationshipName, ConfigurationEntityName, PcfImportEntityName);
    }


    /// <summary>
    /// Determines whether two entities are eligible to participate in a relationship
    /// </summary>
    /// <param name="referencedEntity">Primary Entity</param>
    /// <param name="referencingEntity">Referencing Entity</param>
    /// <returns></returns>
    private bool EligibleCreateOneToManyRelationship(string referencedEntity,
        string referencingEntity)
    {
        //Checks whether the specified entity can be the primary entity in one-to-many
        //relationship.
        var canBeReferencedRequest = new CanBeReferencedRequest
        {
            EntityName = referencedEntity
        };

        var canBeReferencedResponse =
            (CanBeReferencedResponse) _context.Execute(canBeReferencedRequest);

        if (!canBeReferencedResponse.CanBeReferenced)
        {
            _logger.LogInformation(
                "Entity {EName} can't be the primary entity in this one-to-many relationship",
                referencedEntity);
        }

        //Checks whether the specified entity can be the referencing entity in one-to-many
        //relationship.
        var canBeReferencingRequest = new CanBeReferencingRequest
        {
            EntityName = referencingEntity
        };

        CanBeReferencingResponse canBeReferencingResponse =
            (CanBeReferencingResponse)_context.Execute(canBeReferencingRequest);

        if (!canBeReferencingResponse.CanBeReferencing)
        {
            _logger.LogInformation(
                "Entity {EName} can't be the referencing entity in this one-to-many relationship",
                referencingEntity);
        }
        
        return canBeReferencedResponse.CanBeReferenced
               && canBeReferencingResponse.CanBeReferencing;
    }
    #endregion
}