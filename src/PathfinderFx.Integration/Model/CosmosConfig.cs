namespace PathfinderFx.Integration.Model;

public class CosmosConfig: ICosmosConfig
{
    public string? AccountEndpoint { get; set; }
    public string? CosmosDbName { get; set; }
    public string? AuthKey { get; set; }
}
