using System.Text.Json.Serialization;

namespace PathfinderFx.Model;

public class PfResponseEvent
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("specVersion")]
    public string SpecVersion { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("source")]
    public string Source { get; set; }
    
    [JsonPropertyName("data")]
    public PfResponseData Data { get; set; }
    
    public PfResponseEvent(string source, string requestEventId, ProductFootprints pfs)
    {
        Source = source;
        Type = "org.wbcsd.pathfinder.ProductFootprintRequest.Fulfilled.v1";
        SpecVersion = "1.0";
        Id = new Guid().ToString();
        Data = new PfResponseData(requestEventId, pfs);
    }
}

public class PfResponseData
{
    public PfResponseData(string requestEventId, ProductFootprints pfs)
    {
        RequestEventId = requestEventId;
        Pf = pfs;
    }
    public PfResponseData()
    {
    }
    
    [JsonPropertyName("pfs")]
    public ProductFootprints Pf { get; set; }
    
    [JsonPropertyName("requestEventId")]
    public string RequestEventId { get; set; }
}