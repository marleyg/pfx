using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

/// <summary>
/// Units of measure for declared values
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum DeclaredUnit
{
    [Description("Special SI Unit litre ")] Liter = 0,
    [Description("SI Base Unit kilogram ")] Kilogram = 1,
    [Description("Derived Unit from SI Base Unit metre")] CubicMeter = 2,
    [Description("Derived Unit from special SI Unit watt")] KilowattHour = 3,
    [Description("Derived Unit from special SI Unit joule")] Megajoule = 4,
    [Description("Derived Unit from SI Base Units kilogram and metre")] TonKilometer = 5,
    [Description("Derived Unit from SI Base Unit metre")] SquareMeter = 6
}

