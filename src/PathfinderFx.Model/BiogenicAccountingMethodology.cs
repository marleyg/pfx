using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

[JsonConverter(typeof(StringEnumConverter))]
public enum BiogenicAccountingMethodology
{
    [Description("EU Product Environmental Footprint Guide")] Pef,
    [Description("For the Greenhouse Gas Protocol (GHGP) Land sector and Removals Guidance")] Ghgp,
    [Description("For the ISO 14067 standard")] Iso,
    [Description("For the Quantis Accounting for Natural Climate Solutions Guidance")] Quantis,
}