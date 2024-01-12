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
            DataverseConfig.Password = "!HProtagon1$t";
            DataverseConfig.Url = "https://orgbbfc830d.api.crm10.dynamics.com/api/data/v9.2";
            DataverseConfig.UserName = "marleyg@mcfstoday.onmicrosoft.com";
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
            
            var dataversePf = GetDataversePf(footprint);
            var dataversePcf = GetDataversePcf(footprint.Pcf);
            
            _dataverseClient.AddOrUpdateProductFootprint()
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = "Success";
        }

        return result;
    }


    private Msdyn_SustainabilityProductFootprint GetDataversePfEntity(ProductFootprint pf)
    {
        _logger.LogInformation("GetDataversePfEntity called");
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
    private Msdyn_SustainabilityProductCarbonFootprint GetDataversePcfEntity(Pcf footprintPcf)
    {
        _logger.LogInformation("GetDataversePcfEntity called");
        var dataversePcf = new Msdyn_SustainabilityProductCarbonFootprint
        {
            Msdyn_CoveragePercent = Convert.ToDecimal(footprintPcf.Assurance.Coverage),
            Msdyn_GeographyCountry = footprintPcf.GeographyCountry,
            Msdyn_AllocationRulesDescription = footprintPcf.AllocationRulesDescription,
            Msdyn_BiogenicCarbonContent = Convert.ToDecimal(footprintPcf.BiogenicCarbonContent),
            Msdyn_BiogenicCarbonWithdrawal = Convert.ToDecimal(footprintPcf.BiogenicCarbonWithdrawal),
            Msdyn_BoundaryProcessesDescription = footprintPcf.BoundaryProcessesDescription,
            Msdyn_ExemptedEmissionsDescription = footprintPcf.ExemptedEmissionsDescription,
            Msdyn_ExemptedEmissionsPercent = Convert.ToDecimal(footprintPcf.ExemptedEmissionsPercent),
            Msdyn_FossilCarbonContent = Convert.ToDecimal(footprintPcf.FossilCarbonContent),
            Msdyn_GeographyCountrySubdivision = footprintPcf.GeographyCountrySubdivision,
            Msdyn_PackagingEmissionsIncluded = footprintPcf.PackagingEmissionsIncluded,
            Msdyn_PrimaryDataShare = Convert.ToDecimal(footprintPcf.PrimaryDataShare),
            Msdyn_ReferencePeriodStart = footprintPcf.ReferencePeriodStart?.DateTime,
            Msdyn_ReferencePeriodEnd = footprintPcf.ReferencePeriodEnd?.DateTime,
            Msdyn_UncertaintyAssessmentDescription = footprintPcf.UncertaintyAssessmentDescription,
            Msdyn_UnitaryProductAmount = Convert.ToDecimal(footprintPcf.UnitaryProductAmount),
            Msdyn_CompletenessDQR = Convert.ToDecimal(footprintPcf.Dqi.CompletenessDQR),
            Msdyn_GeographicalDQR = Convert.ToDecimal(footprintPcf.Dqi.GeographicalDQR),
            Msdyn_GeographyRegionOrSubregion = footprintPcf.GeographyRegionOrSubregion,
            Msdyn_PcFExcludingBiogenic = Convert.ToDecimal(footprintPcf.PCfExcludingBiogenic),
            Msdyn_PcFIncludingBiogenic = Convert.ToDecimal(footprintPcf.PCfIncludingBiogenic),
            Msdyn_ReliabilityDQR = Convert.ToDecimal(footprintPcf.Dqi.ReliabilityDQR),
            Msdyn_TemporalDQR = Convert.ToDecimal(footprintPcf.Dqi.TemporalDQR),
            Msdyn_TechnologicalDQR = Convert.ToDecimal(footprintPcf.Dqi.TechnologicalDQR),
            Msdyn_AircraftGHGEmissions = Convert.ToDecimal(footprintPcf.AircraftGhgEmissions),
            Msdyn_LandManagementGHGEmissions = Convert.ToDecimal(footprintPcf.LandManagementGhgEmissions),
            Msdyn_OtherBiogenicGHGEmissions = Convert.ToDecimal(footprintPcf.OtherBiogenicGhgEmissions),
            Msdyn_DLuCGHGEmissions = Convert.ToDecimal(footprintPcf.DLucGhgEmissions),
            Msdyn_FossilGHGemIsSiOns = Convert.ToDecimal(footprintPcf.FossilGhgEmissions),
            Msdyn_ILuCGHGEmissions = Convert.ToDecimal(footprintPcf.ILucGhgEmissions),
            Msdyn_PackAgInGgHGEmissions = Convert.ToDecimal(footprintPcf.PackagingGhgEmissions),
            Msdyn
        };

        dataversePcf.Msdyn_BiogenicAccountingMethodology = footprintPcf.BiogenicAccountingMethodology switch
        {
            "ISO" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Iso,
            "GHGP" => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp,
            _ => Msdyn_SustainabilityProductCarbonFootprint_Msdyn_BiogenicAccountingMethodology.Ghgp
        };
        
        //foreach on cross sectorial
        
        //foreach for other collections and enums

        return dataversePcf;
    }
}