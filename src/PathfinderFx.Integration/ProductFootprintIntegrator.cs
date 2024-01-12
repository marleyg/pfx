using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Clients;
using PathfinderFx.Integration.Model;

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
            result.RecordsProcessed++;
            result.Success = true;
            result.Message = "Success";
        }

        return result;
    }
}