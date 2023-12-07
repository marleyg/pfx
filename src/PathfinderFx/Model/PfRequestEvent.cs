using System.Text.Json.Serialization;

namespace PathfinderFx.Model;

public class PfRequestEvent(string source)
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "org.wbcsd.pathfinder.ProductFootprintRequest.Created.v1";

    [JsonPropertyName("specVersion")]
    public string SpecVersion { get; set; } = "1.0";

    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();

    [JsonPropertyName("source")]
    public string Source { get; set; } = source;

    [JsonPropertyName("time")]
    public string Time { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

    [JsonPropertyName("data")]
    public PfRequestData Data { get; set; } = new();
}

public class PfRequestData
{
    [JsonPropertyName("pfIds")]
    public List<string> PfIds { get; set; } = [];
}