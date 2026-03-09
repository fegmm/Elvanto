using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.People;

public class PeopleCustomFieldsTests : BaseTest
{
    [Fact]
    public async Task GetCustomFields_Works()
    {
        var response =
            await client.People.CustomFields.GetAllJson.PostAsync(null,
                TestContext.Current.CancellationToken);

        Assert.True(response?.CustomFieldsQueryResponse?.CustomFields?.CustomField?.All(i => i.Id != null) ?? false);
        Assert.NoAdditionalData(response.CustomFieldsQueryResponse);
    }
}
