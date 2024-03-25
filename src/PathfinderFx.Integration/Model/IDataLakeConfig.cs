namespace PathfinderFx.Integration.Model;

public interface IDataLakeConfig
{
    public string? DataLakeAccountName { get; set; }
    public string? FileSystemName { get; set; }
    
}