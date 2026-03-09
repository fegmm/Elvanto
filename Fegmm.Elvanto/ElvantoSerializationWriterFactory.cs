using System.Text.Json;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;

namespace Fegmm.Elvanto;

internal class ElvantoSerializationWriterFactory() : JsonSerializationWriterFactory(new KiotaJsonSerializationContext(GetElvantoJsonSerializerOptions()))
{
    public static JsonSerializerOptions GetElvantoJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new DateTimeOffsetAsElvantoTimeConverter(),
                new NullableDateTimeOffsetAsElvantoTimeConverter(),
            }
        };
        return options;
    }
}