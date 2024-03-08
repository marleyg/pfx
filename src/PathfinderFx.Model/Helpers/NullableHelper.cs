namespace PathfinderFx.Model.Helpers;

public static class NullableHelper
{
    //
    public static bool GetNullableBool(bool? boolValue)
    {
        return boolValue ?? false;
    }
    
}