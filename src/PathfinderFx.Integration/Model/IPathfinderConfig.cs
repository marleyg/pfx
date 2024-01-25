using Newtonsoft.Json;

namespace PathfinderFx.Integration.Model;

public interface IPathfinderConfig
{
    List<IPathfinderConfigEntry>? PathfinderConfigEntries { get; set; }

    public interface IPathfinderConfigEntry
    {
        [JsonProperty("name")] string? HostName { get; set; }

        [JsonProperty("host_url")] string? HostUrl { get; set; }

        [JsonProperty("auth_url")] string? HostAuthUrl { get; set; }

        [JsonProperty("client_id")] string? ClientId { get; set; }

        [JsonProperty("client_secret")] string? ClientSecret { get; set; }
    }
}