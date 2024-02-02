using Azure;
using Azure.Identity;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;

namespace PathfinderFx.Integration.Clients;

public class FabricClient
{
    
    #region DataLake Storage Operations
    
    public static DataLakeServiceClient GetDataLakeServiceClient(string accountName)
    {
        var dfsUri = $"https://{accountName}.dfs.core.windows.net";

        var dataLakeServiceClient = new DataLakeServiceClient(
            new Uri(dfsUri),
            new DefaultAzureCredential());

        return dataLakeServiceClient;
    }
    
    public async Task<DataLakeFileSystemClient> CreateFileSystem(
        DataLakeServiceClient serviceClient,
        string fileSystemName)
    {
        return await serviceClient.CreateFileSystemAsync(fileSystemName);
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
}