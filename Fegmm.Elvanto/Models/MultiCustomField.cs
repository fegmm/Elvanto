using Fegmm.Elvanto.Models;
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;

public class MultiCustomField : IParsable
{
    public List<IdName>? CustomField { get; set; } = [];

    public IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
    {
        return new Dictionary<string, Action<IParseNode>>
        {
            { "custom_field", n => { CustomField = n.GetCollectionOfObjectValues(IdName.CreateFromDiscriminatorValue)?.AsList(); } },
        };
    }

    public void Serialize(ISerializationWriter writer)
    {
        if(ReferenceEquals(writer, null)) throw new ArgumentNullException(nameof(writer));
        writer.WriteCollectionOfObjectValues("custom_field", CustomField);
    }

    public static MultiCustomField CreateFromDiscriminatorValue(IParseNode parseNode)
    {
        if(ReferenceEquals(parseNode, null)) throw new ArgumentNullException(nameof(parseNode));
        return new MultiCustomField();
    }
}