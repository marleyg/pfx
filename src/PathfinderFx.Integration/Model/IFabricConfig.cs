namespace PathfinderFx.Integration.Model;

public interface IFabricConfig
{
    public string? DataLakeAccountName { get; set; }
    public string? FileSystemName { get; set; }
    
}