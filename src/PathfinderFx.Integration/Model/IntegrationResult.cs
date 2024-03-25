using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PathfinderFx.Integration.Model;

public partial class IntegrationResult
{
    [JsonProperty("recordsProcessed", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public int RecordsProcessed { get; set; }

    [JsonProperty("footprintRecordResults", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public List<FootprintRecordResult> FootprintRecordResults { get; set; } = [];
    
    [JsonProperty("pathfinderHostConnected", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public bool PathfinderHostConnected { get; set; }
    
    [JsonProperty("pathfinderHostMessage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string PathfinderHostMessage { get; set; } = string.Empty;
}

public class FootprintRecordResult
{
    [JsonProperty("success", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public bool Success { get; set; }
    
    [JsonProperty("message", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string DataverseMessage { get; set; } = string.Empty;
    
    [JsonProperty("dataLakeMessage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string DataLakeMessage { get; set; } = string.Empty;
    
    [JsonProperty("cosmosMessage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string CosmosMessage { get; set; } = string.Empty;
}

public partial class IntegrationResult
{
    public static IntegrationResult FromJson(string json) => JsonConvert.DeserializeObject<IntegrationResult>(json, Converter.Settings)!;
}
public static partial class Serialize
{
    public static string ToJson(this IntegrationResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

