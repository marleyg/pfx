using System.Text.Json.Serialization;

namespace PathfinderFx.Model;

public class PfRequestEvent(string source)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("type")]
    public string Type { get; set; } = "org.wbcsd.pathfinder.ProductFootprintRequest.Created.v1";
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("specVersion")]
    public string SpecVersion { get; set; } = "1.0";
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("id")]
    public string Id { get; set; } = new Guid().ToString();
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("source")]
    public string Source { get; set; } = source;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("time")]
    public string Time { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("data")]
    public PfRequestData Data { get; set; } = new();
}

public class PfRequestData
{
    /// <summary>
    /// A product footprint fragment, can be a full or partial footprint for the requested product.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("pf")]
    public PfIds Pf { get; set; } = new();
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }
}

public class PfIds
{
    //example urn:gtin:4712345060507
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("productIds")]
    public List<string> ProductIds { get; set; } = [];
}

