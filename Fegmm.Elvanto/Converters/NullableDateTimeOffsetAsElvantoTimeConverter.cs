using System.Text.Json;
using System.Text.Json.Serialization;

public class NullableDateTimeOffsetAsElvantoTimeConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        return reader.GetDateTimeOffset();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}