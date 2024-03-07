
using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class Extension
{
    [JsonProperty("specVersion")]
    public string SpecVersion { get; set; }

    [JsonProperty("dataSchema")]
    public Uri DataSchema { get; set; }
   
    [JsonProperty("documentation", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Documentation { get; set; }

    [JsonProperty("data")]
    public object Data { get; set; }
}