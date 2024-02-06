namespace PathfinderFx.Integration.Model;

public class IntegrationResult
{
    public string Message { get; set; } = string.Empty;
    public int RecordsProcessed { get; set; }
    public bool Success { get; set; }
    
    public string FabricMessage { get; set; } = string.Empty;
    
    public string CosmosMessage { get; set; } = string.Empty;
}