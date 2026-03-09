using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeOffsetAsElvantoTimeConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDateTimeOffset();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}