using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.People;

public class PeopleCategoriesTests : BaseTest
{
    [Fact]
    public async Task GetCategories_Works()
    {
        // Note: The current OpenAPI spec might cause this to return ErrorResponse or be incorrectly typed
        // depending on the generated code.
        var response = await client.People.Categories.GetAllJson.PostAsync(null, TestContext.Current.CancellationToken);

        Assert.True(response?.PeopleCategoriesResponseQuery?.Categories?.Category?.All(i => i.Id != null) ?? false);
        Assert.NoAdditionalData(response.PeopleCategoriesResponseQuery);
    }
}
