using System.Text.Json.Serialization;

namespace PathfinderFx.Model;

public class PfRequestEvent
{
    public PfRequestEvent(string source)
    {
        Source = source;
        Type = "org.wbcsd.pathfinder.ProductFootprintRequest.Created.v1";
        SpecVersion = "1.0";
        Id = new Guid().ToString();
        Time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        Data = new PfRequestData();
    }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("specVersion")]
    public string SpecVersion { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("source")]
    public string Source { get; set; }
    
    [JsonPropertyName("time")]
    public string Time { get; set; }
    
    [JsonPropertyName("data")]
    public PfRequestData Data { get; set; }
    
}

public class PfRequestData
{
    public PfRequestData()
    {
    }
    public PfRequestData(string pf, string comment)
    {
        Pf = pf;
        Comment = comment;
    }

    [JsonPropertyName("productFootprintFragment")]
    public string Pf { get; set; }
    
    [JsonPropertyName("comment")]
    public string Comment { get; set; }
}