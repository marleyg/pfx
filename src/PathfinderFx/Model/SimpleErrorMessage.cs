using System.Text.Json.Serialization;
using System.Text.Json;

namespace PathfinderFx.Model;

public class SimpleErrorMessage
{
    public SimpleErrorMessage(string message, string code)
    {
        Message = message;
        Code = code;
    }
    [JsonPropertyName("message")]
    public string Message { get; set; } 
    [JsonPropertyName("code")]
    public string Code { get; set; } 
}
public static partial class Serialize
{
    public static string ToJson(this SimpleErrorMessage self) => JsonSerializer.Serialize(self, Converter.Settings);
}