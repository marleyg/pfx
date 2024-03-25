using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Model;

namespace PathfinderFx.Integration.Clients;

public class CosmosDbClient
{
    private readonly ILogger<CosmosDbClient> _logger;
    private readonly ICosmosConfig? _cosmosConfig;
    private readonly CosmosClient _cosmosDbClient;

    public CosmosDbClient(ILoggerFactory myLoggerFactory, ICosmosConfig cosmosConfig)
    {
        _logger = myLoggerFactory.CreateLogger<CosmosDbClient>();
        _cosmosConfig = cosmosConfig;
        //var credential = new DefaultAzureCredential();
        _cosmosDbClient = new CosmosClient(_cosmosConfig.AccountEndpoint, _cosmosConfig.AuthKey);
    }
    
    #region initialization
    
    public async Task<string> InitializeDatabase(IEnumerable<string> hostNames)
    {
        _logger.LogInformation("Creating database {DatabaseName}", _cosmosConfig.CosmosDbName);
        try
        {
            await _cosmosDbClient.CreateDatabaseIfNotExistsAsync(_cosmosConfig.CosmosDbName);
            
            _logger.LogInformation("Creating containers in database {DatabaseName}, for each host", _cosmosConfig.CosmosDbName);
            
            foreach (var hostName in hostNames)
            {
                await _cosmosDbClient.GetDatabase(_cosmosConfig.CosmosDbName).CreateContainerIfNotExistsAsync(hostName, "/id");
            }
            return "Database and containers created successfully";
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating database {DatabaseName}", _cosmosConfig.CosmosDbName);
            return "Error creating Cosmos database and containers";
        }
    }
    
    #endregion
    
    #region CosmosDb Operations
    
    public async Task<string> AddFootprint(string hostName, ProductFootprint footprint)
    {
        _logger.LogInformation("Adding footprint to CosmosDb");
        try
        {
            var container = _cosmosDbClient.GetContainer(_cosmosConfig.CosmosDbName, hostName);
            var result = await container.CreateItemAsync(footprint);
            return result.ETag;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding footprint to CosmosDb");
            return string.Empty;
        }
    }
    
    public async Task<ProductFootprint?> GetFootprint(string hostName, string footprintId)
    {
        _logger.LogInformation("Getting footprint from CosmosDb");
        try
        {
            var container = _cosmosDbClient.GetContainer(_cosmosConfig.CosmosDbName, hostName);
            var result = await container.ReadItemAsync<ProductFootprint>(footprintId, new PartitionKey(footprintId));
            return result.Resource;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting footprint from CosmosDb");
            return null;
        }
    }
    
    public async Task<bool> DeleteFootprint(string hostName, string footprintId)
    {
        _logger.LogInformation("Deleting footprint from CosmosDb");
        try
        {
            var container = _cosmosDbClient.GetContainer(_cosmosConfig.CosmosDbName, hostName);
            await container.DeleteItemAsync<ProductFootprint>(footprintId, new PartitionKey(footprintId));
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting footprint from CosmosDb");
            return false;
        }
    }
    
    #endregion
}