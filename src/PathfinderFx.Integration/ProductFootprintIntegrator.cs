using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Clients;
using PathfinderFx.Integration.Model;

namespace PathfinderFx.Integration;

public class ProductFootprintIntegrator
{
    private readonly ILogger _logger = Utils.AppLogger.MyLoggerFactory.CreateLogger<ProductFootprintIntegrator>();
    private static IPathfinderConfig PathfinderConfig = new PathfinderConfig();
    private static IDataverseConfig DataverseConfig = new DataverseConfig();

    private PathfinderClient _pathfinderClient;
    private DataverseClient _dataverseClient;
    
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
            DataverseConfig.Password = Environment.GetEnvironmentVariable("Password");
            DataverseConfig.Url = Environment.GetEnvironmentVariable("Url");
            DataverseConfig.UserName = Environment.GetEnvironmentVariable("UserName");
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

        var pfs = await _pathfinderClient.FootprintsAsync(null, "", null);
        if (pfs == null)
        {
            result.Success = false;
            result.Message = "Error getting footprints from Pathfinder";
            return result;
        }

        foreach (var footprint in pfs.Data)
        {
            
        }

        return result;
    }
}