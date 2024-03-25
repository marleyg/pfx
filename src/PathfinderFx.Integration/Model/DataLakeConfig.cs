namespace PathfinderFx.Integration.Model;

public class DataLakeConfig : IDataLakeConfig
{
    public string? DataLakeAccountName { get; set; }
    public string? FileSystemName { get; set; }
}