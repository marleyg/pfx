namespace PathfinderFx.Model.Helpers;

public static class EnumHelper
{
    //write a method that returns the text of the enum value
    public static string GetEnumText<T>(T value) where T : Enum
    {
        return Enum.GetName(typeof(T), value) ?? string.Empty;
    }

    public static bool TryParseStatusEnum(string catcherStatus, out Status status)
    {
        try
        {
            status = GetEnumFromText<Status>(catcherStatus);
            return true;
        }
        catch
        {
            status = Status.Active;
            return false;
        }
    }

    public static T GetEnumFromText<T>(string textValue)
    {
        //return the enum value that matches the text after removing any whitespace
        return (T)Enum.Parse(typeof(T), textValue.Replace(" ", ""), true);
    }

    public static bool TryParseDeclaredUnit(string catcherPcfDeclaredUnit, out DeclaredUnit unit)
    {
        try
        {
            unit = GetEnumFromText<DeclaredUnit>(catcherPcfDeclaredUnit);
            return true;
        }
        catch
        {
            unit = DeclaredUnit.Kilogram;
            return false;
        }
    }

    public static bool TryParseGeographicCountry(string catcherPcfGeographyCountry, out GeographyCountry country)
    {
        try
        {
            country = GetEnumFromText<GeographyCountry>(catcherPcfGeographyCountry);
            return true;
        }
        catch
        {
            country = GeographyCountry.GB;
            return false;
        }
    }

    public static bool TryParseRegionOrSubregion(string catcherPcfGeographyRegionOrSubregion, out RegionOrSubregion regionOrSubregion)
    {
        try
        {
            regionOrSubregion = GetEnumFromText<RegionOrSubregion>(catcherPcfGeographyRegionOrSubregion);
            return true;
        }
        catch
        {
            regionOrSubregion = RegionOrSubregion.Europe;
            return false;
        }
    }
    
}