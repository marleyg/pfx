using Newtonsoft.Json;

namespace PathfinderFx.Model;

public class SimpleErrorMessage(string message, string code)
{
    [JsonProperty("message")]
    public string Message { get; set; } = message;

    [JsonProperty("code")]
    public string Code { get; set; } = code;
}
public static partial class Serialize
{
    public static string ToJson(this SimpleErrorMessage self) => JsonConvert.SerializeObject(self, Converter.Settings);
}