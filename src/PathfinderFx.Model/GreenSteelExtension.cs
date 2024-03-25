
using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class GreenSteelExtension
{

    [JsonProperty("steelType", NullValueHandling = NullValueHandling.Ignore)]
    public string SteelType { get; set; }
   
    [JsonProperty("steelProductionProcess", NullValueHandling = NullValueHandling.Ignore)]
    public string SteelProductionProcess { get; set; }

    [JsonProperty("steelProductionLocation", NullValueHandling = NullValueHandling.Ignore)]
    public string SteelProductionLocation { get; set; }

    [JsonProperty("steelProductionDate", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? SteelProductionDate { get; set; }

    [JsonProperty("steelProductionCountry", NullValueHandling = NullValueHandling.Ignore)]
    public GeographyCountry SteelProductionCountry { get; set; }
  
    [JsonProperty("heatTreatment", NullValueHandling = NullValueHandling.Ignore)]
    public string HeatTreatment { get; set; }
        
}