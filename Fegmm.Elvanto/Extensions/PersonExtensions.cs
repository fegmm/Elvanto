using Fegmm.Elvanto.Models;
using Microsoft.Kiota.Abstractions.Serialization;

namespace Fegmm.Elvanto;

public static class PersonExtensions
{
    extension(Person person)
    {
        public async Task<MultiCustomField?> GetMultiOptionCustomField(string customField)
            => person.AdditionalData.TryGetValue(customField, out object? value) &&
                value is UntypedObject customFieldUntyped ?
                await KiotaJsonSerializer.DeserializeAsync<MultiCustomField>(KiotaJsonSerializer.SerializeAsStream(customFieldUntyped)) : null;

        public async Task<IdName?> GetSingleOptionCustomField(string customField)
            => person.AdditionalData.TryGetValue(customField, out object? value) &&
                value is UntypedObject customFieldUntyped ?
                await KiotaJsonSerializer.DeserializeAsync<IdName>(KiotaJsonSerializer.SerializeAsStream(customFieldUntyped)) : null;

        public string? GetStringCustomField(string customField)
            => person.AdditionalData.TryGetValue(customField, out object? value) &&
                value is string strValue ?
                strValue : null;

        public DateOnly? GetDateCustomField(string customField)
        {
            person.AdditionalData.TryGetValue(customField, out object? value);

            return value switch
            {
                DateOnly date => date,
                DateTime dateTime => DateOnly.FromDateTime(dateTime),
                string strValue when DateOnly.TryParse(strValue, out DateOnly date) => date,
                _ => null
            };
        }

        public int? GetNumberCustomField(string customField)
            => person.AdditionalData.TryGetValue(customField, out object? value) &&
                value is int intValue ?
                intValue : null;
    }
}