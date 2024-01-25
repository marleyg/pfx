using Newtonsoft.Json;

namespace PathfinderFx.Integration.Model;

public class PathfinderConfig: IPathfinderConfig
{
    [JsonProperty("pathfinder_config_entries")]
    public List<IPathfinderConfig.IPathfinderConfigEntry>? PathfinderConfigEntries { get; set; } = [];
}

public class PathfinderConfigEntry: IPathfinderConfig.IPathfinderConfigEntry
{
    [JsonProperty("host_name")] public string? HostName { get; set; }

    [JsonProperty("host_url")] public string? HostUrl { get; set; }

    [JsonProperty("host_auth_url")] public string? HostAuthUrl { get; set; }

    [JsonProperty("client_id")] public string? ClientId { get; set; }

    [JsonProperty("client_secret")] public string? ClientSecret { get; set; }
}