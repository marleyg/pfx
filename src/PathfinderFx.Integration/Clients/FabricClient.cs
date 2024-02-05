using System.Text;
using Azure;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using Microsoft.Extensions.Logging;
using PathfinderFx.Integration.Model;

namespace PathfinderFx.Integration.Clients;

public class FabricClient
{
    private readonly ILogger _logger;
    private readonly IFabricConfig _config;
    private readonly DataLakeServiceClient _serviceClient;

    public FabricClient(ILoggerFactory loggerFactory, IFabricConfig config)
    {
        _logger = loggerFactory.CreateLogger<FabricClient>();
        _config = config;
        _serviceClient = CreateServiceClient().Result;
    }

    #region DataLake Storage Operations

    private Task<DataLakeServiceClient> CreateServiceClient()
    {
        var serviceClient = new DataLakeServiceClient(
            new Uri($"https://{_config.DataLakeAccountName}.dfs.core.windows.net"),
            new DefaultAzureCredential());

        return Task.FromResult(serviceClient);
    }
    
    private DataLakeFileSystemClient GetFileSystemClient(string path)
    {
        return  _serviceClient.GetFileSystemClient(path);
    }
    
    public async Task<string> AddFootprint(string hostName, ProductFootprint footprint)
    {
        _logger.LogInformation("Adding footprint to DataLake");
        try{
            var fileSystemClient = GetFileSystemClient(_config.FileSystemName!);
            var directoryClient = fileSystemClient.GetDirectoryClient(hostName);
            var fileClient = directoryClient.GetFileClient(footprint.Id + ".json");
            
            //serialize the footprint into a fileStream
            var footprintJson = ProductFootprint.ToJson(footprint);
            var fileStream = new MemoryStream(Encoding.UTF8.GetBytes(footprintJson));
            var result = await fileClient.UploadAsync(content: fileStream, overwrite: true);
            return result.Value.ETag.ToString();
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error adding footprint to DataLake");
            return string.Empty;
        }
    }
    
    public async Task<ProductFootprint?> GetFootprint(string hostName, string footprintId)
    {
        _logger.LogInformation("Getting footprint from DataLake");
        try
        {
            var fileSystemClient = GetFileSystemClient(_config.FileSystemName!);
            var directoryClient = fileSystemClient.GetDirectoryClient(hostName);
            var fileClient = directoryClient.GetFileClient(footprintId + ".json");
            Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();

            var reader = new BinaryReader(downloadResponse.Value.Content);
            
            var footprintJson = reader.ReadString();
            return ProductFootprint.FromJson(footprintJson);
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error getting footprint from DataLake");
            return null;
        }
    }
    
    public async Task<List<string>> ListFootprintsFromHost(string hostName)
    {
        var fileSystemClient = GetFileSystemClient(_config.FileSystemName!);
        var retVal = new List<string>();
        await using IAsyncEnumerator<PathItem> enumerator =
            fileSystemClient.GetPathsAsync(hostName).GetAsyncEnumerator();

        await enumerator.MoveNextAsync();

        var item = enumerator.Current;

        while (true)
        {
            retVal.Add(item.Name);

            if (!await enumerator.MoveNextAsync())
            {
                break;
            }

            item = enumerator.Current;
        }

        return retVal;
    }
    
    public async Task DeleteHostFootprints(string hostName)
    {
        _logger.LogInformation("Deleting footprints for host {HostName}", hostName);
        var fileSystemClient = GetFileSystemClient(_config.FileSystemName!);
        var directoryClient =
            fileSystemClient.GetDirectoryClient(hostName);

        await directoryClient.DeleteAsync();
    }
    
    #endregion

    #region Fabric Setup
    public async Task<bool> InitializeFabricConfiguration(IEnumerable<string> fabricHostNames)
    {
        _logger.LogInformation("Initializing Fabric Configuration");
        
        try
        {
            //create a container for the pathfinder footprints
            //create a directory for each host
            var fileSystemClient = await CreateFileSystem(_config.FileSystemName!);
            foreach(var host in fabricHostNames)
            {
                await CreateDirectory(fileSystemClient, host);
            }
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error initializing Fabric Configuration");
            return false;
        }
        
        return true;
    }
    
    private async Task<DataLakeFileSystemClient> CreateFileSystem(string fileSystemName)
    {
        try
        {
            DataLakeFileSystemClient? retVal = await _serviceClient.CreateFileSystemAsync(fileSystemName);
            return retVal;
        }
        catch(RequestFailedException e)
        {
            if (e.Status == 409)
            {
                _logger.LogInformation("FileSystem already exists");
                return _serviceClient.GetFileSystemClient(fileSystemName);
            }

            _logger.LogError(e, "Error creating FileSystem");
            throw;
        }
    }
    
    private static async Task CreateDirectory(DataLakeFileSystemClient fileSystemClient,
        string directoryName)
    {
        await fileSystemClient.CreateDirectoryAsync(directoryName);
    }

    
    #endregion
}