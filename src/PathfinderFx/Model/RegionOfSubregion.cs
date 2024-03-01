using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PathfinderFx.Model;

/// <summary>
/// Regions or subregions
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum RegionOrSubregion
{
    [Description("Africa")] Africa = 1,
    [Description("Americas")] Americas = 2,
    [Description("Asia")] Asia= 3,
    [Description("Europe")] Europe = 4,
    [Description("Oceania")] Oceania = 5,
    [Description("Australia and New Zealand")] AustraliaAndNewZealand = 6,
    [Description("Central Asia")] CentralAsia = 7,
    [Description("Eastern Asia")] EasternAsia = 8,
    [Description("Eastern Europe")] EasternEurope = 9,
    [Description("Latin America and the Caribbean")] LatinAmericaAndTheCaribbean = 10,
    [Description("Melanesia")] Melanesia = 11,
    [Description("Micronesia")] Micronesia = 12,
    [Description("Northern Africa")] NorthernAfrica = 13,
    [Description("Northern America")] NorthernAmerica = 14,
    [Description("Northern Europe")] NorthernEurope = 15,
    [Description("Polynesia")] Polynesia = 16,
    [Description("South-eastern Asia")] SouthEasternAsia = 17,
    [Description("Southern Asia")] SouthernAsia = 18,
    [Description("Southern Europe")] SouthernEurope = 19,
    [Description("Sub-Saharan Africa")] SubSaharanAfrica = 20,
    [Description("Western Asia")] WesternAsia = 21,
    [Description("Western Europe")] WesternEurope = 22,
}