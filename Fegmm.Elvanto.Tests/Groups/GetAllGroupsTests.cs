using Fegmm.Elvanto.Groups.GetAllJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Groups;

public class GetAllGroupsTests : BaseTest
{
    [Fact]
    public async Task GetAll_AllAdditionalFields_Works()
    {
        GetAllPostRequestBody body = new()
        {
            Fields = [.. Enum.GetValues<GroupAdditionalFields>()]
        };
        var response = await client.Groups.GetAllJson.PostAsync(body, null, TestContext.Current.CancellationToken);

        Assert.NotNull(response?.GroupQueryResponse?.Groups?.Group);
        Assert.NotEmpty(response.GroupQueryResponse.Groups.Group);
        Assert.NoAdditionalData(response.GroupQueryResponse);
    }
}
