using System.Text.Json.Serialization;
using System.Text.Json;

namespace PathfinderFx.Model;

public class SimpleErrorMessage(string message, string code)
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = message;

    [JsonPropertyName("code")]
    public string Code { get; set; } = code;
}
public static partial class Serialize
{
    public static string ToJson(this SimpleErrorMessage self) => JsonSerializer.Serialize(self, Converter.Settings);
}