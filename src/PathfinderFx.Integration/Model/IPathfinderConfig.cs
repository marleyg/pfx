using Newtonsoft.Json;

namespace PathfinderFx.Integration.Model;

public interface IPathfinderConfig
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