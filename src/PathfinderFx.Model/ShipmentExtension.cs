using Newtonsoft.Json;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public class ShipmentExtension
{
    [JsonProperty("shipmentId", NullValueHandling = NullValueHandling.Ignore)]
    public string ShipmentId { get; set; }

    [JsonProperty("consignmentId", NullValueHandling = NullValueHandling.Ignore)]
    public string ConsignmentId { get; set; }

    [JsonProperty("shipmentType", NullValueHandling = NullValueHandling.Ignore)]
    public string ShipmentType { get; set; }

    [JsonProperty("weight", NullValueHandling = NullValueHandling.Ignore)]
    public long? Weight { get; set; }

    [JsonProperty("transportChainElementId", NullValueHandling = NullValueHandling.Ignore)]
    public string TransportChainElementId { get; set; }
}