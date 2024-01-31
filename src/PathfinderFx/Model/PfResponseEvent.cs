using System.Text.Json.Serialization;

namespace PathfinderFx.Model;


public class PfResponseEvent
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "org.wbcsd.pathfinder.ProductFootprintRequest.Fulfilled.v1";

    [JsonPropertyName("specVersion")]
    public string SpecVersion { get; set; } = "1.0";

    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("data")]
    public PfResponseData? Data { get; set; } = new();
}

public class PfResponseData
{
    [JsonPropertyName("pfs")]
    public ProductFootprints? Pf { get; set; }

    [JsonPropertyName("requestEventId")]
    public string? RequestEventId { get; set; }
}