using System.Text;
using Microsoft.Extensions.Logging;
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
            var dataversePcf = GetDataversePcfEntity(footprint);
            
            var resultMsg = _dataverseClient.AddOrUpdateProductFootprint(dataversePf, dataversePcf);
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = resultMsg;
        }

        return result;
    }

    private Msdyn_SustainabilityProductFootprint GetDataversePfEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePfEntity called");
        
        //check to see if the product already exists in Dataverse
        var existingPf = _dataverseClient!.GetProductFootprintByOriginCorrelationId(pf.Id.ToString());
        
        if (existingPf != null)
        {
            _logger.LogInformation("ProductFootprint {ProductFootprintId} already exists in Dataverse", pf.Id);
            return UpdatePfLocalEntityData(existingPf, pf);
        }
        var dataversePf = new Msdyn_SustainabilityProductFootprint
        {
            Msdyn_SustainabilityProductFootprintId = pf.Id,
            Msdyn_Name = pf.ProductNameCompany,
            Msdyn_Comment = pf.Comment,
            Msdyn_Version = (int?)pf.Version,
            Msdyn_SpecVersion = pf.SpecVersion,
            Msdyn_StatusComment = pf.StatusComment,
            Msdyn_OriginCorrelationId = pf.Id.ToString()!,
            Msdyn_ValidityPeriodStart = pf.ValidityPeriodStart?.DateTime,
            Msdyn_ValidityPeriodEnd = pf.ValidityPeriodEnd?.DateTime
        };
        return dataversePf;
    }

    private Msdyn_SustainabilityProductFootprint UpdatePfLocalEntityData(Msdyn_SustainabilityProductFootprint existingPf, ProductFootprint footprint)
    {
        _logger.LogInformation("UpdatePfLocalEntityData called for ProductFootprint {ProductFootprintId}", footprint.Id);
        //update the existingPf from the values in footprint
        existingPf.Msdyn_Name = footprint.ProductNameCompany;
        existingPf.Msdyn_Comment = footprint.Comment;
        existingPf.Msdyn_Version = (int?)footprint.Version;
        existingPf.Msdyn_SpecVersion = footprint.SpecVersion;
        existingPf.Msdyn_StatusComment = footprint.StatusComment;
        existingPf.Msdyn_OriginCorrelationId = footprint.Id.ToString()!;
        existingPf.Msdyn_ValidityPeriodStart = footprint.ValidityPeriodStart?.DateTime;
        existingPf.Msdyn_ValidityPeriodEnd = footprint.ValidityPeriodEnd?.DateTime;
        return existingPf;
    }

    private Msdyn_SustainabilityProductCarbonFootprint GetDataversePcfEntity(ProductFootprint footprint)
    {
        _logger.LogInformation("GetDataversePcfEntity called");
        
        //check to see if the product already exists in Dataverse
        var existingPcf = _dataverseClient!.GetProductCarbonFootprintByOriginCorrelationId(footprint.Id.ToString());
        if(existingPcf != null)
        {
            _logger.LogInformation("ProductCarbonFootprint {ProductCarbonFootprintId} already exists in Dataverse", footprint.Id);
            return UpdatePcfLocalEntityData(existingPcf, footprint);
        }
        
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
            Msdyn_Name = footprint.ProductNameCompany,
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

    private Msdyn_SustainabilityProductCarbonFootprint UpdatePcfLocalEntityData(Msdyn_SustainabilityProductCarbonFootprint existingPcf, ProductFootprint footprint)
    {
        _logger.LogInformation("UpdatePcfLocalEntityData called for ProductCarbonFootprint {ProductCarbonFootprintId}", footprint.Id);
        
        //update the existingPcf from the values in footprint.Pcf
        existingPcf.Msdyn_GeographyCountry = footprint.Pcf.GeographyCountry;
        existingPcf.Msdyn_AllocationRulesDescription = footprint.Pcf.AllocationRulesDescription;
        existingPcf.Msdyn_BiogenicCarbonContent = Convert.ToDecimal(footprint.Pcf.BiogenicCarbonContent);
        existingPcf.Msdyn_BiogenicCarbonWithdrawal = Convert.ToDecimal(footprint.Pcf.BiogenicCarbonWithdrawal);
        existingPcf.Msdyn_BoundaryProcessesDescription = footprint.Pcf.BoundaryProcessesDescription;
        existingPcf.Msdyn_ExemptedEmissionsDescription = footprint.Pcf.ExemptedEmissionsDescription;
        existingPcf.Msdyn_ExemptedEmissionsPercent = Convert.ToDecimal(footprint.Pcf.ExemptedEmissionsPercent);
        existingPcf.Msdyn_FossilCarbonContent = Convert.ToDecimal(footprint.Pcf.FossilCarbonContent);
        existingPcf.Msdyn_GeographyCountrySubdivision = footprint.Pcf.GeographyCountrySubdivision;
        existingPcf.Msdyn_PackagingEmissionsIncluded = footprint.Pcf.PackagingEmissionsIncluded;
        existingPcf.Msdyn_PrimaryDataShare = Convert.ToDecimal(footprint.Pcf.PrimaryDataShare);
        existingPcf.Msdyn_ReferencePeriodStart = footprint.Pcf.ReferencePeriodStart?.DateTime;
        existingPcf.Msdyn_ReferencePeriodEnd = footprint.Pcf.ReferencePeriodEnd?.DateTime;
        existingPcf.Msdyn_UncertaintyAssessmentDescription = footprint.Pcf.UncertaintyAssessmentDescription;
        existingPcf.Msdyn_UnitaryProductAmount = Convert.ToDecimal(footprint.Pcf.UnitaryProductAmount);
        existingPcf.Msdyn_GeographyRegionOrSubregion = footprint.Pcf.GeographyRegionOrSubregion;
        existingPcf.Msdyn_PcFExcludingBiogenic = Convert.ToDecimal(footprint.Pcf.PCfExcludingBiogenic);
        existingPcf.Msdyn_PcFIncludingBiogenic = Convert.ToDecimal(footprint.Pcf.PCfIncludingBiogenic);
        existingPcf.Msdyn_AircraftGHGEmissions = Convert.ToDecimal(footprint.Pcf.AircraftGhgEmissions);
        existingPcf.Msdyn_LandManagementGHGEmissions = Convert.ToDecimal(footprint.Pcf.LandManagementGhgEmissions);
        existingPcf.Msdyn_OtherBiogenicGHGEmissions = Convert.ToDecimal(footprint.Pcf.OtherBiogenicGhgEmissions);
        existingPcf.Msdyn_DLuCGHGEmissions = Convert.ToDecimal(footprint.Pcf.DLucGhgEmissions);
        existingPcf.Msdyn_FossilGHGemIsSiOns = Convert.ToDecimal(footprint.Pcf.FossilGhgEmissions);
        existingPcf.Msdyn_ILuCGHGEmissions = Convert.ToDecimal(footprint.Pcf.ILucGhgEmissions);
        existingPcf.Msdyn_PackAgInGgHGEmissions = Convert.ToDecimal(footprint.Pcf.PackagingGhgEmissions);
        existingPcf.Msdyn_Name = footprint.ProductNameCompany;
        existingPcf.Msdyn_SustainabilityProductCarbonFootprintId = footprint.Id;
        existingPcf.Msdyn_BiogenicAccountingMethodology = footprint.Pcf.BiogenicAccountingMethodology switch
        {
            "ISO" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Iso,
            "GHGP" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
            "GGP" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
            "PEF" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Pef,
            "Quantis" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Quantis,
            _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp
        };
        
        //foreach on footprint.Pcf.CharacterizationFactors
        existingPcf.Msdyn_CharacterizationFactors = footprint.Pcf.CharacterizationFactors switch
        {
            "AR5" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar5,
            "AR6" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6,
            _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_CharacterizationFactors.Ar6
        };
        
        if (footprint.Pcf.Dqi != null)
        {
            existingPcf.Msdyn_CoveragePercent = Convert.ToDecimal(footprint.Pcf.Dqi.CoveragePercent);
            existingPcf.Msdyn_CompletenessDQR = Convert.ToDecimal(footprint.Pcf.Dqi.CompletenessDQR);
            existingPcf.Msdyn_GeographicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.GeographicalDQR);
            existingPcf.Msdyn_ReliabilityDQR = Convert.ToDecimal(footprint.Pcf.Dqi.ReliabilityDQR);
            existingPcf.Msdyn_TemporalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TemporalDQR);
            existingPcf.Msdyn_TechnologicalDQR = Convert.ToDecimal(footprint.Pcf.Dqi.TechnologicalDQR);
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
        existingPcf.Msdyn_SecondaryEmissionFactorSources = secondaryEmissionFactorSources.ToString();

        return existingPcf;
    }
}