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
    private static readonly IFabricConfig FabricConfig = new FabricConfig();

    private PathfinderClient? _pathfinderClient;
    private DataverseClient? _dataverseClient;
    private FabricClient? _fabricClient;
    private List<Msdyn_Unit>? _units;
    private IPathfinderConfig.IPathfinderConfigEntry? _currentPathfinderConfigEntry;
    
    #region constructors
    public ProductFootprintIntegrator()
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetDataverseConfiguration();
        SetFabricConfiguration();
        SetPathfinderConfiguration();
    }

    public ProductFootprintIntegrator(IPathfinderConfig? pathfinderConfig = null, IDataverseConfig? dataverseConfig = null, IFabricConfig? fabricConfig = null, bool initialization = false)
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetDataverseConfiguration(dataverseConfig);
        SetFabricConfiguration(fabricConfig);
        
        if (initialization) return;
        SetPathfinderConfiguration(pathfinderConfig);
    }

    #endregion

    #region configuration
    
    /// <summary>
    /// Get a list of available Pathfinder hosts
    /// </summary>
    /// <returns></returns>
    public List<string> GetPathfinderHosts()
    {
        _logger.LogInformation("GetPathfinderHosts called");
        return PathfinderConfig.PathfinderConfigEntries!.Select(entry => entry.HostName!).ToList();
    }
    
    /// <summary>
    /// Set the Pathfinder host to use for the integration from the list of registered hosts
    /// </summary>
    /// <param name="hostName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string SetCurrentPathfinderHost(string hostName)
    {
        _logger.LogInformation("SetCurrentPathfinderHost called");
        var host = PathfinderConfig.PathfinderConfigEntries!.FirstOrDefault(entry => entry.HostName == hostName);
        if (host == null)
        {
            _logger.LogError("Host {HostName} not found", hostName);
            throw new Exception("Host not found");
        }
        _currentPathfinderConfigEntry = host;
        _pathfinderClient = new PathfinderClient(Utils.AppLogger.MyLoggerFactory, _currentPathfinderConfigEntry);
        return "Current host set to: " + hostName;
    }
    
    private void SetPathfinderConfiguration(IPathfinderConfig? config = null)
    {
        _logger.LogInformation("SetPathfinderConfiguration called");
        if (config == null)
        {
            try
            {
                var pathfinderConfigFromDataverse = _dataverseClient?.GetPathfinderFxConfiguration();
                if (pathfinderConfigFromDataverse == null)
                {
                    _logger.LogError("Pathfinder configuration is required and not found in Dataverse");
                    throw new Exception("Pathfinder configuration is required and not found in Dataverse");
                }

                if (pathfinderConfigFromDataverse.Count == 0)
                {
                    _logger.LogInformation("Pathfinder configuration is required for full operation and not found in Dataverse, initialization is available");
                    return;
                }

                config = new PathfinderConfig();
                foreach (var cfgData in pathfinderConfigFromDataverse)
                {
                    config.PathfinderConfigEntries?.Add(new PathfinderConfigEntry
                    {
                        HostAuthUrl = cfgData.Msdyn_HostAuthUrl,
                        ClientId = cfgData.Msdyn_ClientId,
                        ClientSecret = cfgData.Msdyn_ClientSecret,
                        HostUrl = cfgData.Msdyn_HostBaseUrl,
                        HostName = cfgData.Msdyn_HostName
                    });
                }

                _currentPathfinderConfigEntry = config.PathfinderConfigEntries!.FirstOrDefault();
                PathfinderConfig.PathfinderConfigEntries = config.PathfinderConfigEntries;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting Pathfinder configuration from Dataverse");
                throw new Exception("Error getting Pathfinder configuration from Dataverse");
            }
        }
        else
        {
            try
            {
                PathfinderConfig.PathfinderConfigEntries = config.PathfinderConfigEntries;
                _currentPathfinderConfigEntry = config.PathfinderConfigEntries!.FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing configuration");
                throw new Exception("Error deserializing configuration");
            }
        }
        _pathfinderClient = new PathfinderClient(Utils.AppLogger.MyLoggerFactory, _currentPathfinderConfigEntry);
    }

    
    private void SetFabricConfiguration(IFabricConfig? fabricConfig = null)
    {
        if (fabricConfig == null)
        {
            _logger.LogError("Fabric configuration is required");
            throw new Exception("Fabric configuration is required");
        }
        FabricConfig.DataLakeAccountName = fabricConfig.DataLakeAccountName;
        FabricConfig.FileSystemName = fabricConfig.FileSystemName;
        
        _fabricClient = new FabricClient(Utils.AppLogger.MyLoggerFactory, FabricConfig);
    }
    
    private void SetDataverseConfiguration(IDataverseConfig? dataverseConfig = null)
    {
        _logger.LogInformation("SetDataverseConfiguration called");
        if (dataverseConfig == null)
        {
            _logger.LogError("Dataverse configuration is required");
            throw new Exception("Dataverse configuration is required");
        }

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
            _logger.LogError(e, "Error in Dataverse configuration");
            throw new Exception("Error in Dataverse configuration");
        }
        _dataverseClient = new DataverseClient(Utils.AppLogger.MyLoggerFactory, DataverseConfig);
    }

    #endregion
    
    public async Task<IntegrationResult> IntegrateProductFootprints(bool cleanDataverse = false, string hostToUse = "", bool includeFabric = false)
    {
        _logger.LogInformation("IntegrateProductFootprints called");

        if(!string.IsNullOrEmpty(hostToUse))
            SetCurrentPathfinderHost(hostToUse);
        if (cleanDataverse)
        {
            _logger.LogInformation("Cleaning Dataverse of ProductFootprints");
            var cleanResult = _dataverseClient?.CleanDataverseTables();
            _logger.LogInformation("Clean result: {Result}", cleanResult);
        }
        
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
        
        _logger.LogInformation("Getting units from Dataverse");
        _units = _dataverseClient?.GetUnits();
        
        
        _logger.LogInformation("Processing footprints for Dataverse");
        _logger.LogInformation("Accessing Dataverse as {WhoAmI}", _dataverseClient!.WhoAmI());
        foreach (var footprint in pfs.Data)
        {
            _logger.LogInformation("Processing footprint {FootprintId}", footprint.Id);
            
            //dataverse processing
            var dataversePf = GetDataversePfEntity(footprint);
            if (dataversePf == null) continue;
            var resultMsg = _dataverseClient.AddProductFootprint(dataversePf);
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = resultMsg;
            
            //fabric processing
            if (!includeFabric) continue;
            _logger.LogInformation("Processing footprint {FootprintId} for Fabric", footprint.Id);
            var hostName = _currentPathfinderConfigEntry!.HostName;
            if (hostName == null) continue;
            var fabricResult =
                await _fabricClient?.AddFootprint(hostName, footprint)!;
            result.FabricMessage = fabricResult;
        }
        return result;
    }


    #region entity collections
    private ProductFootprintEntityCollection? GetDataversePfEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePfEntity called");
        
        //check to see if the product already exists in Dataverse
        var existingPf = _dataverseClient!.GetProductFootprintIdentifierById(pf.Id);
        
        if (existingPf != null)
        {
            _logger.LogInformation("ProductFootprint {ProductFootprintId} already exists in Dataverse", pf.Id);
            return null;
        }

        var dataversePfEntityCollection = new ProductFootprintEntityCollection
        {
            HostName = _currentPathfinderConfigEntry?.HostName
        };
        
        //get the SustainabilityProductIdentifier entity from the ProductFootprint
        var identifier = new Msdyn_SustainabilityProductIdentifier
        {
            Msdyn_Name = pf.Id.ToString(),
            Msdyn_SustainabilityProductIdentifierId = pf.Id
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
        var dataversePfRm = GetProductRuleOrSectorRules(pf);
        dataversePfEntityCollection.Msdyn_ProductOrSectorSpecificRule = dataversePfRm;
        
        //get the ProductCarbonFootprintAssurance entity from the ProductFootprint
        var dataversePcfA = GetDataversePcfAssuranceEntity(pf);
        dataversePfEntityCollection.Msdyn_ProductCarbonFootprintAssurance = dataversePcfA;
        
        return dataversePfEntityCollection;
    }

    private Msdyn_ProductCarbonFootprintAssurance? GetDataversePcfAssuranceEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePcfAssuranceEntity called for {ProductFootprintId}", pf.Id);
        if (pf.Pcf.Assurance == null) return null;
        var assuranceData = pf.Pcf.Assurance;
        var assurance = new Msdyn_ProductCarbonFootprintAssurance
        {
            Msdyn_ProviderName = assuranceData.ProviderName,
            Msdyn_Comments = assuranceData.Comments,
            Msdyn_StandardName = assuranceData.StandardName,
            Msdyn_CompletedDate = assuranceData.CompletedAt?.DateTime,
            Msdyn_Name = pf.Id.ToString(),
            Msdyn_ProductCarbonFootprintAssuranceId = pf.Id,
            Msdyn_Boundary = assuranceData.Boundary switch
            {
                "Cradle-to-Gate" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Boundary.CradleToGate,
                "Gate-to-Gate" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Boundary.GateToGate,
                _ => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Boundary.CradleToGate
            },
            Msdyn_Coverage = assuranceData.Coverage.ToLower() switch
            {
                "product line" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Coverage.ProductLine,
                "product level" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Coverage.ProductLevel,
                "corporate level" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Coverage.CorporateLevel,
                "pcf system" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Coverage.PcfSystem,
                _ => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Coverage.ProductLine
            },
            Msdyn_Level = assuranceData.Level.ToLower() switch
            {
                "limited" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Level.Limited,
                "reasonable" => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Level.Reasonable,
                _ => Msdyn_ProductCarbonFootprintAssurance_Msdyn_Level.Limited
            }
        };

        return assurance;
    }

    private static List<Msdyn_ProductOrSectorSpecificRule> GetProductRuleOrSectorRules(ProductFootprint pf)
    {
        var rules = new List<Msdyn_ProductOrSectorSpecificRule>();
        foreach (var rule in pf.Pcf.ProductOrSectorSpecificRules)
        {
            var newRule = new Msdyn_ProductOrSectorSpecificRule
            {
                Msdyn_OtherOperatorName = rule.Operator,
            };
            var ruleNames = new StringBuilder();
            foreach(var name in rule.RuleNames)
            {
                if (ruleNames.Length > 0)
                {
                    ruleNames.Append(',');
                }
                ruleNames.Append(name);
            }
            newRule.Msdyn_RuleNames = ruleNames.ToString();
            rules.Add(newRule);
        }
        return rules;
    }


    private static Msdyn_SustainabilityProduct GetSustainabilityProduct(ProductFootprint pf)
    {
        var product = new Msdyn_SustainabilityProduct
        {
            Msdyn_Name = pf.Id.ToString(),
            Msdyn_ProductDescription = pf.ProductNameCompany + ": " + pf.ProductDescription,
            Msdyn_ProductCategoryCPc = Convert.ToString(pf.ProductCategoryCpc) ?? string.Empty,
            Msdyn_SustainabilityProductId = pf.Id
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
        product.Msdyn_ProductDescription += ", Company Product Ids: " + productIds;

        return product;
    }

    private static Msdyn_SustainabilityProductFootprint GetSustainabilityProductFootprint(ProductFootprint pf)
    {
        var dataversePf = new Msdyn_SustainabilityProductFootprint
        {
            Msdyn_SustainabilityProductFootprintId = pf.Id,
            Msdyn_Name = pf.Id.ToString(),
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
        dataversePf.Msdyn_Comment += ", Preceding Product Footprint Ids:  " + precedingPfIds;

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
            Msdyn_Name = footprint.Id.ToString(),
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

        if (!string.IsNullOrEmpty(footprint.Pcf.DeclaredUnit))
        {
            var searchUnit = footprint.Pcf.DeclaredUnit.ToLower();
            if (searchUnit == "liter")
                searchUnit = "Litres";
            var unit = _units?.FirstOrDefault(u => string.Equals(u.Msdyn_Name, searchUnit, StringComparison.CurrentCultureIgnoreCase));
            if (unit != null)
            {
                dataversePcf.Msdyn_DeclaredUnit = new EntityReference(Msdyn_Unit.EntityLogicalName, (Guid)unit.Msdyn_UnitId!);
            }
        }
        
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
    
    #endregion
    
    #region Initialize or clean Pathfinder Configuration

        
    public Task<string> CleanDataverseTables()
    {
        _logger.LogInformation("CleanDataverseTables called");
        var result = _dataverseClient?.CleanDataverseTables();
        return Task.FromResult(result ?? "Error cleaning Dataverse tables");
    }
    
    public string CreatePathfinderConfiguration()
    {
        _logger.LogInformation("CreatePathfinderConfiguration called");

        var result = new StringBuilder();
        
        result.Append("Dataverse Initialized: " + _dataverseClient!.InitializePathfinderFxConfiguration());
        
        //get the Hostnames from the PathfinderConfig
        var hostNames = PathfinderConfig.PathfinderConfigEntries!.Select(entry => entry.HostName!).ToList();
        
        result.AppendLine("Fabric Initialized: " + _fabricClient!.InitializeFabricConfiguration(hostNames).Result);
        return result.ToString();
    }
    #endregion
}