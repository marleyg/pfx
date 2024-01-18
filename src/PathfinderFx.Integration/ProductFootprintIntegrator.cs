using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using PathfinderFx.Integration.Clients;
using PathfinderFx.Integration.Model;
using PathfinderFx.Integration.Model.Entities;

namespace PathfinderFx.Integration;

public class ProductFootprintIntegrator
{
    private readonly ILogger _logger = Utils.AppLogger.MyLoggerFactory.CreateLogger<ProductFootprintIntegrator>();
    private static readonly IPathfinderConfig PathfinderConfig = new PathfinderConfig();
    private static readonly IDataverseConfig DataverseConfig = new DataverseConfig();

    private PathfinderClient? _pathfinderClient;
    private DataverseClient? _dataverseClient;
    
    public ProductFootprintIntegrator()
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetPathfinderConfiguration();
        SetDataverseConfiguration();
    }

    public ProductFootprintIntegrator(IPathfinderConfig? pathfinderConfig = null, IDataverseConfig? dataverseConfig = null)
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetPathfinderConfiguration(pathfinderConfig);
        SetDataverseConfiguration(dataverseConfig);
    }

        
    public bool SetPathfinderConfiguration(IPathfinderConfig? config = null)
    {
        _logger.LogInformation("SetPathfinderConfiguration called");
        if (config == null)
        {
            PathfinderConfig.AuthUrl = Environment.GetEnvironmentVariable("AuthUrl");
            PathfinderConfig.ClientId = Environment.GetEnvironmentVariable("ClientId");
            PathfinderConfig.ClientSecret = Environment.GetEnvironmentVariable("ClientSecret");
            PathfinderConfig.HostUrl = Environment.GetEnvironmentVariable("HostUrl");
        }
        else
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing configuration");
                return false;
            }
        }
        _pathfinderClient = new PathfinderClient(Utils.AppLogger.MyLoggerFactory, PathfinderConfig);
        return true;
    }
    
    public bool SetDataverseConfiguration(IDataverseConfig? dataverseConfig = null)
    {
        _logger.LogInformation("SetDataverseConfiguration called");
        if (dataverseConfig == null)
        {
            DataverseConfig.Password = "!HProtagoni$t";
            DataverseConfig.Url = "https://org44b772fa.api.crm.dynamics.com/api/data/v9.2";
            DataverseConfig.UserName = "Marley@HMDemoJune22.onmicrosoft.com";
            Environment.SetEnvironmentVariable("Password", DataverseConfig.Password);
            Environment.SetEnvironmentVariable("Url", DataverseConfig.Url);
            Environment.SetEnvironmentVariable("UserName", DataverseConfig.UserName);
        }
        else
        {
            try
            {
                DataverseConfig.Password = dataverseConfig.Password;
                DataverseConfig.Url = dataverseConfig.Url;
                DataverseConfig.UserName = dataverseConfig.UserName;
                
                Environment.SetEnvironmentVariable("ConnectionString", dataverseConfig.ConnectionString);
                Environment.SetEnvironmentVariable("Password", dataverseConfig.Password);
                Environment.SetEnvironmentVariable("Url", dataverseConfig.Url);
                Environment.SetEnvironmentVariable("UserName", dataverseConfig.UserName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing configuration");
                return false;
            }
        }
        _dataverseClient = new DataverseClient(Utils.AppLogger.MyLoggerFactory, DataverseConfig);
        return true;
    }

    public async Task<IntegrationResult> IntegrateProductFootprints()
    {
        _logger.LogInformation("IntegrateProductFootprints called");
        
        var result = new IntegrationResult();

        _logger.LogInformation("Getting footprints from Pathfinder");
        var pfs = await _pathfinderClient?.FootprintsAsync(null, null, null)!;
        if (pfs == null)
        {
            result.Success = false;
            result.Message = "Error getting footprints from Pathfinder";
            return result;
        }
        _logger.LogInformation("Got {Count} footprints from Pathfinder", pfs.Data.Count);
        
        _logger.LogInformation("Processing footprints for Dataverse");
        _logger.LogInformation("Accessing Dataverse as {WhoAmI}", _dataverseClient!.WhoAmI());
        foreach (var footprint in pfs.Data)
        {
            _logger.LogInformation("Processing footprint {FootprintId}", footprint.Id);
            
            var dataversePf = GetDataversePfEntity(footprint);
            if (dataversePf == null) continue;
            var resultMsg = _dataverseClient.AddOrUpdateProductFootprint(dataversePf);
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = resultMsg;
        }

        return result;
    }

    private ProductFootprintEntityCollection? GetDataversePfEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePfEntity called");
        
        //check to see if the product already exists in Dataverse
        var existingPf = _dataverseClient!.GetProductFootprintByOriginCorrelationId(pf.Id.ToString());
        
        if (existingPf != null)
        {
            _logger.LogInformation("ProductFootprint {ProductFootprintId} already exists in Dataverse", pf.Id);
            return null;
        }

        var dataversePfEntityCollection = new ProductFootprintEntityCollection();
        
        //get the SustainabilityProductIdentifier entity from the ProductFootprint
        var identifier = new Msdyn_SustainabilityProductIdentifier
        {
            Msdyn_SustainabilityProductIdentifierId = pf.Id,
            Msdyn_Name = pf.Id.ToString() ?? string.Empty,
        };
        dataversePfEntityCollection.Msdyn_SustainabilityProductIdentifier = identifier;

        //get the SustainabilityProduct entity from the ProductFootprint
        var product = GetSustainabilityProduct(pf);
        dataversePfEntityCollection.Msdyn_SustainabilityProduct = product;
        
        //get the SustainabilityProductFootprint entity from the ProductFootprint
        var dataversePf = GetSustainabilityProductFootprint(pf);
        dataversePfEntityCollection.Msdyn_SustainabilityProductFootprint = dataversePf;
        
        //get the SustainabilityProductCarbonFootprint entity from the ProductFootprint
        var dataversePcf = GetDataversePcfEntity(pf);
        dataversePfEntityCollection.Msdyn_SustainabilityProductCarbonFootprint = dataversePcf;
        
        //get the ProductFootprintRuleMapping entity from the ProductFootprint
        var dataversePfRm = GetProductRuleOrSectorRuleAndMappingEntities(pf);
        dataversePfEntityCollection.Msdyn_ProductOrSectorSpecificRule = dataversePfRm.rules;
        dataversePfEntityCollection.Msdyn_ProductFootprintRuleMapping = dataversePfRm.mapping;
        
        //get the ProductCarbonFootprintAssurance entity from the ProductFootprint
        var dataversePcfA = GetDataversePcfAssuranceEntity(pf);
        dataversePfEntityCollection.Msdyn_ProductCarbonFootprintAssurance = dataversePcfA;
        
        return dataversePfEntityCollection;
    }

    private Msdyn_ProductCarbonFootprintAssurance GetDataversePcfAssuranceEntity(ProductFootprint pf)
    {
        throw new NotImplementedException();
    }

    private (List<Msdyn_ProductOrSectorSpecificRule> rules, Msdyn_ProductFootprintRuleMapping mapping) GetProductRuleOrSectorRuleAndMappingEntities(ProductFootprint pf)
    {
        var rules = new List<Msdyn_ProductOrSectorSpecificRule>();
        foreach (var rule in pf.Pcf.ProductOrSectorSpecificRules)
        {
            var newRule = new Msdyn_ProductOrSectorSpecificRule
            {
                Msdyn_OtherOperatorName = rule.Operator,
            };
            var ruleName = new StringBuilder();
            foreach(var name in rule.RuleNames)
            {
                if (ruleName.Length > 0)
                {
                    ruleName.Append(',');
                }
                ruleName.Append(name);
            }
            rules.Add(newRule);
        }
        
        var mapping = new Msdyn_ProductFootprintRuleMapping
        {
            Msdyn_ProductFootprintRuleMappingId = pf.Id,
            Msdyn_Name = pf.Id.ToString() ?? string.Empty,
        };
        return (rules, mapping);
    }


    private static Msdyn_SustainabilityProduct GetSustainabilityProduct(ProductFootprint pf)
    {
        var product = new Msdyn_SustainabilityProduct
        {
            Msdyn_Name = pf.ProductNameCompany,
            Msdyn_ProductDescription = pf.ProductDescription,
            Msdyn_ProductCategoryCPc = Convert.ToString(pf.ProductCategoryCpc) ?? string.Empty,
            Msdyn_SustainabilityProductId = pf.Id,
        };

        if (pf.ProductIds == null) return product;
        var productIds = new StringBuilder();
        foreach (var productId in pf.ProductIds)
        {
            if (productIds.Length > 0)
            {
                productIds.Append(',');
            }
            productIds.Append(productId);
        }
        product.Msdyn_OriginCorrelationId = productIds.ToString();

        return product;
    }

    private static Msdyn_SustainabilityProductFootprint GetSustainabilityProductFootprint(ProductFootprint pf)
    {
        var dataversePf = new Msdyn_SustainabilityProductFootprint
        {
            Msdyn_SustainabilityProductFootprintId = pf.Id,
            Msdyn_Name = pf.Id.ToString() ?? string.Empty,
            Msdyn_Comment = pf.Comment,
            Msdyn_Version = (int?)pf.Version,
            Msdyn_SpecVersion = pf.SpecVersion,
            Msdyn_StatusComment = pf.StatusComment,
            Msdyn_ValidityPeriodStart = pf.ValidityPeriodStart?.DateTime,
            Msdyn_ValidityPeriodEnd = pf.ValidityPeriodEnd?.DateTime,
            Msdyn_FootprintStatus = pf.Status switch
            {
                "Draft" => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Active,
                "Published" => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Inactive,
                _ => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Active
            }
        };

        if (pf.PrecedingPfIds == null) return dataversePf;
        var precedingPfIds = new StringBuilder();
        foreach (var precedingPfId in pf.PrecedingPfIds)
        {
            if (precedingPfIds.Length > 0)
            {
                precedingPfIds.Append(',');
            }
            precedingPfIds.Append(precedingPfId);
        }
        dataversePf.Msdyn_OriginCorrelationId = precedingPfIds.ToString();

        return dataversePf;
    }

    private Msdyn_SustainabilityProductCarbonFootprint GetDataversePcfEntity(ProductFootprint footprint)
    {
        _logger.LogInformation("GetDataversePcfEntity called");
        
        var dataversePcf = new Msdyn_SustainabilityProductCarbonFootprint
        {
            Msdyn_GeographyCountry = footprint.Pcf.GeographyCountry,
            Msdyn_AllocationRulesDescription = footprint.Pcf.AllocationRulesDescription,
            Msdyn_BiogenicCarbonContent = Convert.ToDecimal(footprint.Pcf.BiogenicCarbonContent),
            Msdyn_BiogenicCarbonWithdrawal = Convert.ToDecimal(footprint.Pcf.BiogenicCarbonWithdrawal),
            Msdyn_BoundaryProcessesDescription = footprint.Pcf.BoundaryProcessesDescription,
            Msdyn_ExemptedEmissionsDescription = footprint.Pcf.ExemptedEmissionsDescription,
            Msdyn_ExemptedEmissionsPercent = Convert.ToDecimal(footprint.Pcf.ExemptedEmissionsPercent),
            Msdyn_FossilCarbonContent = Convert.ToDecimal(footprint.Pcf.FossilCarbonContent),
            Msdyn_GeographyCountrySubdivision = footprint.Pcf.GeographyCountrySubdivision,
            Msdyn_PackagingEmissionsIncluded = footprint.Pcf.PackagingEmissionsIncluded,
            Msdyn_PrimaryDataShare = Convert.ToDecimal(footprint.Pcf.PrimaryDataShare),
            Msdyn_ReferencePeriodStart = footprint.Pcf.ReferencePeriodStart?.DateTime,
            Msdyn_ReferencePeriodEnd = footprint.Pcf.ReferencePeriodEnd?.DateTime,
            Msdyn_UncertaintyAssessmentDescription = footprint.Pcf.UncertaintyAssessmentDescription,
            Msdyn_UnitaryProductAmount = Convert.ToDecimal(footprint.Pcf.UnitaryProductAmount),
            Msdyn_GeographyRegionOrSubregion = footprint.Pcf.GeographyRegionOrSubregion,
            Msdyn_PcFExcludingBiogenic = Convert.ToDecimal(footprint.Pcf.PCfExcludingBiogenic),
            Msdyn_PcFIncludingBiogenic = Convert.ToDecimal(footprint.Pcf.PCfIncludingBiogenic),
            Msdyn_AircraftGHGEmissions = Convert.ToDecimal(footprint.Pcf.AircraftGhgEmissions),
            Msdyn_LandManagementGHGEmissions = Convert.ToDecimal(footprint.Pcf.LandManagementGhgEmissions),
            Msdyn_OtherBiogenicGHGEmissions = Convert.ToDecimal(footprint.Pcf.OtherBiogenicGhgEmissions),
            Msdyn_DLuCGHGEmissions = Convert.ToDecimal(footprint.Pcf.DLucGhgEmissions),
            Msdyn_FossilGHGemIsSiOns = Convert.ToDecimal(footprint.Pcf.FossilGhgEmissions),
            Msdyn_ILuCGHGEmissions = Convert.ToDecimal(footprint.Pcf.ILucGhgEmissions),
            Msdyn_PackAgInGgHGEmissions = Convert.ToDecimal(footprint.Pcf.PackagingGhgEmissions),
            Msdyn_Name = footprint.Id.ToString() ?? string.Empty,
            Msdyn_SustainabilityProductCarbonFootprintId = footprint.Id,
            Msdyn_BiogenicAccountingMethodology = footprint.Pcf.BiogenicAccountingMethodology switch
            {
                "ISO" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Iso,
                "GHGP" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
                "GGP" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
                "PEF" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Pef,
                "Quantis" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Quantis,
                _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp
            },
            //foreach on footprint.Pcf.CharacterizationFactors
            Msdyn_CharacterizationFactors = footprint.Pcf.CharacterizationFactors switch
            {
                "AR5" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar5,
                "AR6" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6,
                _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6
            }
        };

        if (footprint.Pcf.Dqi != null)
        {
            dataversePcf.Msdyn_CoveragePercent = Convert.ToDecimal(footprint.Pcf.Dqi.CoveragePercent);
            dataversePcf.Msdyn_CompletenessDQR = Convert.ToDecimal(footprint.Pcf.Dqi.CompletenessDQR);
            dataversePcf.Msdyn_GeographicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.GeographicalDQR);
            dataversePcf.Msdyn_ReliabilityDQR = Convert.ToDecimal(footprint.Pcf.Dqi.ReliabilityDQR);
            dataversePcf.Msdyn_TemporalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TemporalDQR);
            dataversePcf.Msdyn_TechnologicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TechnologicalDQR);
        }

        //list of secondary emission factor sources from the collection
        var secondaryEmissionFactorSources = new StringBuilder();
        foreach (var secEm in footprint.Pcf.SecondaryEmissionFactorSources)
        {
            if (secondaryEmissionFactorSources.Length > 0)
            {
                secondaryEmissionFactorSources.Append(',');
            }
            secondaryEmissionFactorSources.Append(secEm.Name + ": " + secEm.Version);
        }
        dataversePcf.Msdyn_SecondaryEmissionFactorSources = secondaryEmissionFactorSources.ToString();
        
        return dataversePcf;
    }
}