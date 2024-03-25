using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

[JsonConverter(typeof(StringEnumConverter))]
public enum ProductOrSectorSpecificRuleOperator
{
    [Description("For EU/PEF Methodology PCRs")] Pef,
    [Description("For PCRs authored of published by EPD International")] EpdInternational,
    [Description("Any other valid rule operator")] Other
}