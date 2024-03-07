using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;
//https://github.com/wbcsd/data-exchange-protocol/pull/46
[JsonConverter(typeof(StringEnumConverter))]
public enum CrossSectoralStandard
{
    [Description("GHG Protocol Product standard")] GhgProtocolProductStandard,
    [Description("ISO Standard 14067")] IsoStandard14067,
    [Description("ISO Standard 14044")] IsoStandard14044,
}