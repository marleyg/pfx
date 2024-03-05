using Newtonsoft.Json;

namespace PathfinderFx.Model;

public class PfRequestEvent(string source)
{
    [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
    public string Type { get; set; } = "org.wbcsd.pathfinder.ProductFootprintRequest.Created.v1";

    [JsonProperty("specVersion", NullValueHandling = NullValueHandling.Ignore)]
    public string SpecVersion { get; set; } = "1.0";
 
    [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
    public string Id { get; set; } = new Guid().ToString();
    
    [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
    public string Source { get; set; } = source;
    
    [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
    public string Time { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    
    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public PfRequestData Data { get; set; } = new();
}

public class PfRequestData
{
    /// <summary>
    /// A product footprint fragment, can be a full or partial footprint for the requested product.
    /// </summary>
    [JsonProperty("pf", NullValueHandling = NullValueHandling.Ignore)]
    public PfIds Pf { get; set; } = new();
   
    [JsonProperty("comment", NullValueHandling = NullValueHandling.Ignore)]
    public string? Comment { get; set; }
}

public class PfIds
{
    //example urn:gtin:4712345060507
    [JsonProperty("productIds", NullValueHandling = NullValueHandling.Ignore)]
    public List<string> ProductIds { get; set; } = [];
}

