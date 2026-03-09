using Fegmm.Elvanto.Groups.CreateJson;
using Fegmm.Elvanto.Groups.GetInfoJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Groups;

public class CreateGroupTests : BaseTest
{
    [Fact]
    public async Task Create_Group_Works()
    {
        var request = new CreatePostRequestBody
        {
            Name = $"API Test - {TestContext.Current.Test!.TestDisplayName}",
            Status = GroupState.Active
        };
        await using var context = await new GroupContext(client) { SkipTestIfResourceCreationFails = false }
            .Create(request);
        // the creation response should not contain any extra data
        Assert.NotNull(context.CreationResponse);
        Assert.NoAdditionalData(context.CreationResponse);
        var groupId = context.Resource;
        Assert.False(string.IsNullOrEmpty(groupId));

        // Verify by getting info
        var infoResponse = await client.Groups.GetInfoJson.PostAsync(new() { Id = groupId },
            null, TestContext.Current.CancellationToken);

        Assert.Equal(request.Name, infoResponse?.GroupResponse?.Group?.Single()?.Name);
    }
}