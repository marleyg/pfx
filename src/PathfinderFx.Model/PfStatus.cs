using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

[JsonConverter(typeof(StringEnumConverter))]
public enum PfStatus
{
    Deprecated, Invalid
}