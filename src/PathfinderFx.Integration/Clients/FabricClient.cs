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

    private async Task<DataLakeFileSystemClient> CreateFileSystem(string fileSystemName)
    {
        return await _serviceClient.CreateFileSystemAsync(fileSystemName);
    }
    
    private DataLakeFileSystemClient GetFileSystemClient(string path)
    {
        return  _serviceClient.GetFileSystemClient(path);
    }

    private async Task<DataLakeDirectoryClient> CreateDirectory(
        DataLakeFileSystemClient fileSystemClient,
        string directoryName,
        string subdirectoryName)
    {
        DataLakeDirectoryClient directoryClient =
            await fileSystemClient.CreateDirectoryAsync(directoryName);

        return await directoryClient.CreateSubDirectoryAsync(subdirectoryName);
    }
    
    
    public async Task UploadFile(
        DataLakeDirectoryClient directoryClient,
        string fileName,
        string localPath)
    {
        var fileClient = 
            directoryClient.GetFileClient(fileName);

        var fileStream = File.OpenRead(localPath);

        await fileClient.UploadAsync(content: fileStream, overwrite: true);
    }
    
    public async Task DownloadFile(
        DataLakeDirectoryClient directoryClient,
        string fileName,
        string localPath)
    {
        var fileClient =
            directoryClient.GetFileClient(fileName);

        Response<FileDownloadInfo> downloadResponse = await fileClient.ReadAsync();

        var reader = new BinaryReader(downloadResponse.Value.Content);

        var fileStream = File.OpenWrite(localPath);

        const int bufferSize = 4096;

        var buffer = new byte[bufferSize];

        int count;

        while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
        {
            fileStream.Write(buffer, 0, count);
        }

        await fileStream.FlushAsync();

        fileStream.Close();
    }
    
    public async Task ListFilesInDirectory(
        DataLakeFileSystemClient fileSystemClient,
        string directoryName)
    {
        await using IAsyncEnumerator<PathItem> enumerator =
            fileSystemClient.GetPathsAsync(directoryName).GetAsyncEnumerator();

        await enumerator.MoveNextAsync();

        var item = enumerator.Current;

        while (true)
        {
            Console.WriteLine(item.Name);

            if (!await enumerator.MoveNextAsync())
            {
                break;
            }

            item = enumerator.Current;
        }

    }
    
    public async Task DeleteDirectory(
        DataLakeFileSystemClient fileSystemClient,
        string directoryName)
    {
        var directoryClient =
            fileSystemClient.GetDirectoryClient(directoryName);

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
                await CreateDirectory(fileSystemClient, host, "footprints");
            }
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error initializing Fabric Configuration");
            return false;
        }
        
        return true;
    }
    
    #endregion
}