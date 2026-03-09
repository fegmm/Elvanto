using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections;
using System.Reflection;

namespace Fegmm.Elvanto.Tests.Utils;

public static class AssertExtensions
{
    extension(Assert assert)
    {
        public static void NoAdditionalData(object? obj)
        {
            if (obj is not IAdditionalDataHolder holder)
            {
                return;
            }

            Assert.Empty(holder.AdditionalData);

            var properties = holder.GetType()
                .GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == nameof(IAdditionalDataHolder.AdditionalData))
                {
                    continue;
                }

                if (typeof(IAdditionalDataHolder).IsAssignableFrom(property.PropertyType))
                {
                    var value = property.GetValue(obj);
                    Assert.NoAdditionalData(value);
                }
                else if (IsCollectionOfAdditionalDatas(property.PropertyType) && property.GetValue(obj) is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        if (item is IAdditionalDataHolder)
                        {
                            Assert.NoAdditionalData(item);
                        }
                    }
                }
            }
        }
    }

    private static bool IsCollectionOfAdditionalDatas(Type t)
        => typeof(IEnumerable).IsAssignableFrom(t) &&
            typeof(IAdditionalDataHolder).IsAssignableFrom(t.IsArray ? t.GetElementType() : t.GetGenericArguments().FirstOrDefault());
}
