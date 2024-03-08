using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using PathfinderFx.Integration.Clients;
using PathfinderFx.Integration.Model;
using PathfinderFx.Integration.Model.Entities;
using PathfinderFx.Model;
using PathfinderFx.Model.Helpers;

namespace PathfinderFx.Integration;

public class ProductFootprintIntegrator
{
    private readonly ILogger _logger = Utils.AppLogger.MyLoggerFactory.CreateLogger<ProductFootprintIntegrator>();
    private static IPathfinderConfig _pathfinderConfig = new PathfinderConfig();
    private static readonly IDataverseConfig DataverseConfig = new DataverseConfig();

    private PathfinderClient? _pathfinderClient;
    private DataverseClient? _dataverseClient;
    private List<Msdyn_Unit>? _units;
    private IPathfinderConfig.IPathfinderConfigEntry? _currentPathfinderConfigEntry;
    
    #region constructors
    public ProductFootprintIntegrator()
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetDataverseConfiguration();
        SetPathfinderConfiguration();
    }

    public ProductFootprintIntegrator(IPathfinderConfig? pathfinderConfig = null, IDataverseConfig? dataverseConfig = null, bool initialization = false)
    {
        _logger.LogInformation("ProductFootprintIntegrator constructor called");
        SetDataverseConfiguration(dataverseConfig);
        if(!initialization)
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
        return _pathfinderConfig.PathfinderConfigEntries!.Select(entry => entry.HostName!).ToList();
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
        var host = _pathfinderConfig.PathfinderConfigEntries!.FirstOrDefault(entry => entry.HostName == hostName);
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

                _pathfinderConfig = new PathfinderConfig();
                foreach (var cfgData in pathfinderConfigFromDataverse)
                {
                    _pathfinderConfig.PathfinderConfigEntries?.Add(new PathfinderConfigEntry
                    {
                        HostAuthUrl = cfgData.Msdyn_HostAuthUrl,
                        ClientId = cfgData.Msdyn_ClientId,
                        ClientSecret = cfgData.Msdyn_ClientSecret,
                        HostUrl = cfgData.Msdyn_HostBaseUrl,
                        HostName = cfgData.Msdyn_HostName
                    });
                }

                _currentPathfinderConfigEntry = _pathfinderConfig.PathfinderConfigEntries!.FirstOrDefault();
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
                _pathfinderConfig.PathfinderConfigEntries = config.PathfinderConfigEntries;
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
    
    public async Task<IntegrationResult> IntegrateProductFootprints(bool cleanDataverse = false)
    {
        _logger.LogInformation("IntegrateProductFootprints called");

        if (cleanDataverse)
        {
            _logger.LogInformation("Cleaning Dataverse of ProductFootprints");
            var cleanResult = _dataverseClient?.CleanDataverseTables();
            _logger.LogInformation("Clean result: {Result}", cleanResult);
        }
        
        var result = new IntegrationResult();

        _logger.LogInformation("Getting footprints from Pathfinder");
        var productFootprintCatchers = await _pathfinderClient?.FootprintsAsync(null, null, null)!;
        if (productFootprintCatchers == null)
        {
            result.Success = false;
            result.Message = "Error getting footprints from Pathfinder";
            return result;
        }
        _logger.LogInformation("Got {Count} footprints from Pathfinder", productFootprintCatchers.Data.Count);
        
        _logger.LogInformation("Getting units from Dataverse");
        _units = _dataverseClient?.GetUnits();
        
        
        _logger.LogInformation("Processing footprints for Dataverse");
        _logger.LogInformation("Accessing Dataverse as {WhoAmI}", _dataverseClient!.WhoAmI());
        foreach (var caughtFootprint in productFootprintCatchers.Data)
        {
            _logger.LogInformation("Processing footprint {FootprintId}", caughtFootprint.Id);

            ProductFootprint footprint;
            try
            {
                footprint = FootprintPreProcessor.ConvertToProductFootprint(caughtFootprint);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error converting footprint {FootprintId}", caughtFootprint.Id);
                result.RecordsProcessed++;
                result.Success = false;
                result.Message = "Error converting footprint " + caughtFootprint.Id;
                continue;
            }
            
            var dataversePf = GetDataversePfEntity(footprint);
            if (dataversePf == null) continue;
            var resultMsg = _dataverseClient.AddProductFootprint(dataversePf);
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = resultMsg;
        }
        return result;
    }
    
    public Task<string> CleanDataverseTables()
    {
        _logger.LogInformation("CleanDataverseTables called");
        var result = _dataverseClient?.CleanDataverseTables();
        return Task.FromResult(result ?? "Error cleaning Dataverse tables");
    }

    #region entity collections

    private ProductFootprintEntityCollection? GetDataversePfEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePfEntity called");

        //check to see if the product already exists in Dataverse
        var existingPf = _dataverseClient!.GetProductFootprintIdentifierById(new Guid(pf.Id));

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
            Msdyn_Name = pf.Id,
            Msdyn_SustainabilityProductIdentifierId = new Guid(pf.Id)
        };
        dataversePfEntityCollection.Msdyn_SustainabilityProductIdentifier = identifier;

        try
        {
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
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (pf.Pcf.ProductOrSectorSpecificRules != null)
            {
                var dataversePfRm = GetProductRuleOrSectorRules(pf);
                dataversePfEntityCollection.Msdyn_ProductOrSectorSpecificRule = dataversePfRm;
            }

            //get the ProductCarbonFootprintAssurance entity from the ProductFootprint
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (pf.Pcf.Assurance == null) return dataversePfEntityCollection;
            var dataversePcfA = GetDataversePcfAssuranceEntity(pf);
            dataversePfEntityCollection.Msdyn_ProductCarbonFootprintAssurance = dataversePcfA;

            return dataversePfEntityCollection;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating ProductFootprintEntityCollection for {ProductFootprintId}", pf.Id);
            return null;
        }
    }

    private Msdyn_ProductCarbonFootprintAssurance? GetDataversePcfAssuranceEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePcfAssuranceEntity called for {ProductFootprintId}", pf.Id);
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (pf.Pcf.Assurance == null) return null;
        var assuranceData = pf.Pcf.Assurance;
        var assurance = new Msdyn_ProductCarbonFootprintAssurance
        {
            Msdyn_ProviderName = assuranceData.ProviderName,
            Msdyn_Comments = assuranceData.Comments,
            Msdyn_StandardName = assuranceData.StandardName,
            Msdyn_CompletedDate = assuranceData.CompletedAt?.DateTime,
            Msdyn_Name = pf.Id.ToString(),
            Msdyn_ProductCarbonFootprintAssuranceId = new Guid(pf.Id),
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
                Msdyn_OtherOperatorName = rule.OtherOperatorName,
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
            Msdyn_SustainabilityProductId = new Guid(pf.Id)
        };

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
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
            Msdyn_SustainabilityProductFootprintId = new Guid(pf.Id),
            Msdyn_Name = pf.Id,
            Msdyn_Comment = pf.Comment,
            Msdyn_Version = (int?)pf.Version,
            Msdyn_SpecVersion = pf.SpecVersion,
            Msdyn_StatusComment = pf.StatusComment,
            Msdyn_ValidityPeriodStart = pf.ValidityPeriodStart?.DateTime,
            Msdyn_ValidityPeriodEnd = pf.ValidityPeriodEnd?.DateTime,
            Msdyn_FootprintStatus = EnumHelper.GetEnumText(pf.Status) switch
            {
                "Active" => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Active,
                "Depreciated" => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Inactive,
                _ => Msdyn_SustainabilityProductFootprint_Msdyn_FootprintStatus.Active
            }
        };

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
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
            Msdyn_GeographyCountry = EnumHelper.GetEnumText(footprint.Pcf.GeographyCountry),
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
            Msdyn_GeographyRegionOrSubregion = EnumHelper.GetEnumText(footprint.Pcf.GeographyRegionOrSubregion),
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
            Msdyn_SustainabilityProductCarbonFootprintId = new Guid(footprint.Id),
            Msdyn_BiogenicAccountingMethodology = EnumHelper.GetEnumText(footprint.Pcf.BiogenicAccountingMethodology).ToLower() switch
            {
                "iso" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Iso,
                "ghgp" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
                "pef" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Pef,
                "quantis" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Quantis,
                _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp
            },
            //foreach on footprint.Pcf.CharacterizationFactors
            Msdyn_CharacterizationFactors = EnumHelper.GetEnumText(footprint.Pcf.CharacterizationFactors).ToLower() switch
            {
                "ar5" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar5,
                "ar6" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6,
                _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6
            }
        };

        if (!string.IsNullOrEmpty(EnumHelper.GetEnumText(footprint.Pcf.DeclaredUnit)))
        {
            var searchUnit = EnumHelper.GetEnumText(footprint.Pcf.DeclaredUnit).ToLower();
            if (searchUnit == "liter")
                searchUnit = "Litres";
            if (searchUnit == "kilogram")
                searchUnit = "kg";
            if (searchUnit == "cubicmeter")
                searchUnit = "cubic metres";
            if (searchUnit == "kilowatthour")
                searchUnit = "kWh";
            if (searchUnit == "megajoule")
                searchUnit = "J";
            if (searchUnit == "tonkilometer")
                searchUnit = "tonne-km";
            if (searchUnit == "squaremeter")
                searchUnit = "sq m";
            var unit = _units?.FirstOrDefault(u => string.Equals(u.Msdyn_Name, searchUnit, StringComparison.CurrentCultureIgnoreCase));
            if (unit != null)
            {
                dataversePcf.Msdyn_DeclaredUnit = new EntityReference(Msdyn_Unit.EntityLogicalName, (Guid)unit.Msdyn_UnitId);
            }
            //find a unit that contains the first two letters of the searchUnit
            else
            {
                //get the first two letters of the searchUnit
                searchUnit = searchUnit[..2];
                unit = _units?.FirstOrDefault(u => u.Msdyn_Name.ToLower().Contains(searchUnit, StringComparison.CurrentCultureIgnoreCase));
                if (unit != null)
                {
                    dataversePcf.Msdyn_DeclaredUnit = new EntityReference(Msdyn_Unit.EntityLogicalName, (Guid)unit.Msdyn_UnitId);
                }
                else
                {
                    _logger.LogInformation("Unit {Unit} not found, attempting to add it to the msdyn_unit table", EnumHelper.GetEnumText(footprint.Pcf.DeclaredUnit));
                    try
                    {
                        var newUnitId = _dataverseClient?.AddUnit(footprint.Pcf.DeclaredUnit);
                        if (newUnitId != null)
                        {
                            dataversePcf.Msdyn_DeclaredUnit =
                                new EntityReference(Msdyn_Unit.EntityLogicalName, (Guid)newUnitId);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Error adding unit {Unit} to the msdyn_unit table, using the 1st unit in the table", EnumHelper.GetEnumText(footprint.Pcf.DeclaredUnit));
                        unit =  _units?.FirstOrDefault();
                        dataversePcf.Msdyn_DeclaredUnit = new EntityReference(Msdyn_Unit.EntityLogicalName, (Guid)unit.Msdyn_UnitId);
                    }
                }
            }
        }
        
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (footprint.Pcf.Dqi != null)
        {
            dataversePcf.Msdyn_CoveragePercent = Convert.ToDecimal(footprint.Pcf.Dqi.CoveragePercent);
            dataversePcf.Msdyn_CompletenessDQR = Convert.ToDecimal(footprint.Pcf.Dqi.CompletenessDqr);
            dataversePcf.Msdyn_GeographicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.GeographicalDqr);
            dataversePcf.Msdyn_ReliabilityDQR = Convert.ToDecimal(footprint.Pcf.Dqi.ReliabilityDqr);
            dataversePcf.Msdyn_TemporalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TemporalDqr);
            dataversePcf.Msdyn_TechnologicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TechnologicalDqr);
        }

        //list of secondary emission factor sources from the collection
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if(footprint.Pcf.SecondaryEmissionFactorSources == null) return dataversePcf;
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
    
    #region Initialize Pathfinder Configuration

    public string CreatePathfinderConfiguration(bool justImports = false)
    {
        _logger.LogInformation("CreatePathfinderConfiguration called");
        var result =_dataverseClient.InitializePathfinderFxConfiguration();
        return "Initialization: " + result;
    }
    #endregion
}