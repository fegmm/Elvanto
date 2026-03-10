using System.Text.Json;
using Fegmm.Elvanto.Converters;
using Microsoft.Kiota.Serialization.Json;

namespace Fegmm.Elvanto;

internal class ElvantoParseNodeFactory() : JsonParseNodeFactory(new KiotaJsonSerializationContext(GetElvantoJsonSerializerOptions()))
{
    public static JsonSerializerOptions GetElvantoJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new EmptyStringToNullConverter()
            }
        };
        return options;
    }
}