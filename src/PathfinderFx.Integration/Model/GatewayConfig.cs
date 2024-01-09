namespace PathfinderFx.Integration.Model;

public class GatewayConfig: IGatewayConfig
{
    public string? HostUrl { get; set; }
    public string? AuthUrl { get; set; }
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
}