
using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class Assurance
{
    [JsonProperty("assurance")]
    public bool HasAssurance { get; set; }
    
    [JsonProperty("coverage", NullValueHandling = NullValueHandling.Ignore)]
    public string Coverage { get; set; }

    [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
    public string Level { get; set; }

    [JsonProperty("boundary", NullValueHandling = NullValueHandling.Ignore)]
    public string Boundary { get; set; }

    [JsonProperty("providerName", NullValueHandling = NullValueHandling.Ignore)]
    public string ProviderName { get; set; }

    [JsonProperty("completedAt", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? CompletedAt { get; set; }

    [JsonProperty("standardName", NullValueHandling = NullValueHandling.Ignore)]
    public string StandardName { get; set; }

    [JsonProperty("comments", NullValueHandling = NullValueHandling.Ignore)]
    public string Comments { get; set; }
}