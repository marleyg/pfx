using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

[JsonConverter(typeof(StringEnumConverter))]
public enum CharacterizationFactor
{
    [Description("Sixth Assessment Report of the Intergovernmental Panel on Climate Change (IPCC)")] Ar6,
    [Description("Fifth Assessment Report of the Intergovernmental Panel on Climate Change (IPCC)")] Ar5,
}