using Newtonsoft.Json;

namespace PathfinderFxGateway.Model;

public interface IGatewayConfig
{
    [JsonProperty("host_url")]
    string? HostUrl { get; set; }
        
    [JsonProperty("auth_url")]
    string? AuthUrl { get; set; }
        
    [JsonProperty("client_id")]
    string? ClientId { get; set; }
        
    [JsonProperty("client_secret")]
    string? ClientSecret { get; set; }
}