using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PathfinderFx.Model;

#pragma warning disable CS8618, CS8601, CS8603
public static class Converter
{
    public static readonly JsonSerializerSettings Settings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}


internal class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}
    
    public class DateOnlyConverter(string? serializationFormat) : JsonConverter<DateOnly>
    {
        private readonly string _serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        public DateOnlyConverter() : this(null) { }

        public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(_serializationFormat));
        }

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return DateOnly.Parse(value);
        }
    }

    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }


        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(serializationFormat));
        }

        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return TimeOnly.Parse(value);
        }
    }
    