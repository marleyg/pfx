namespace PathfinderFx.Helper;

public static class EnumerationExtensions
{
    public static string AsText<T>(this T value) where T : Enum
    {
        return Enum.GetName(typeof(T), value) ?? string.Empty;
    }
}