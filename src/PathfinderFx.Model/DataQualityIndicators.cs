

using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class DataQualityIndicators
{
    [JsonProperty("coveragePercent")]
    public long? CoveragePercent { get; set; }

    [JsonProperty("technologicalDQR")]
    public double? TechnologicalDqr { get; set; }

    [JsonProperty("temporalDQR")]
    public double? TemporalDqr { get; set; }

    [JsonProperty("geographicalDQR")]
    public long? GeographicalDqr { get; set; }

    [JsonProperty("completenessDQR")]
    public double? CompletenessDqr { get; set; }

    [JsonProperty("reliabilityDQR")]
    public double? ReliabilityDqr { get; set; }
}