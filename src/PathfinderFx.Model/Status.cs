using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

[JsonConverter(typeof(StringEnumConverter))]
public enum Status
{
    [Description("The default status of a product footprint is Active. A product footprint with status Active CAN be used by a data recipients, e.g. for product footprint calculations")] Active,
    [Description("The product footprint is deprecated and SHOULD NOT be used for e.g. product footprint calculations by data recipients")] Depreciated,
}