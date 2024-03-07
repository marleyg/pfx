namespace PathfinderFx.Model.Helpers;

public static class EnumHelper
{
    //write a method that returns the text of the enum value
    public static string GetEnumText<T>(T value) where T : Enum
    {
        return Enum.GetName(typeof(T), value) ?? string.Empty;
    }
}