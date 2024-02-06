namespace PathfinderFx.Integration.Model;

public interface ICosmosConfig
{
    public string? AccountEndpoint { get; set; }
    public string? CosmosDbName { get; set; }
    
    public string? AuthKey { get; set; }
}