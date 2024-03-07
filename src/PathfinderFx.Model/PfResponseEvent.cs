using Newtonsoft.Json;

namespace PathfinderFx.Model;


public class PfResponseEvent
{
    [JsonProperty("type")]
    public string Type { get; set; } = "org.wbcsd.pathfinder.ProductFootprintRequest.Fulfilled.v1";

    [JsonProperty("specVersion")]
    public string SpecVersion { get; set; } = "1.0";

    [JsonProperty("id")]
    public string Id { get; set; } = new Guid().ToString();

    [JsonProperty("source")]
    public string? Source { get; set; }

    [JsonProperty("data")]
    public PfResponseData? Data { get; set; } = new();
}

public class PfResponseData
{
    [JsonProperty("pfs")]
    public ProductFootprints? Pf { get; set; }

    [JsonProperty("requestEventId")]
    public string? RequestEventId { get; set; }
}