using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

/// <summary>
/// Units of measure for declared values
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum DeclaredUnit
{
    Liter = 0,
    Kilogram = 1,
    CubicMeter = 2,
    KilowattHour = 3,
    Megajoule = 4,
    TonKilometer = 5,
    SquareMeter = 6
}

