using Fegmm.Elvanto.Groups.CreateJson;
using Fegmm.Elvanto.Groups.RemoveJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Groups;

public class RemoveGroupTests : BaseTest
{
    [Fact]
    public async Task Remove_Group_Works()
    {
        // Create manually
        var createBody = new CreatePostRequestBody
        {
            Name = $"API Test - {TestContext.Current.Test!.TestDisplayName}",
            Status = GroupState.Active
        };

        await using var context = await new GroupContext(client).Create(createBody);
        var groupId = context.Resource;
        await context.DisposeAsync();

        Assert.NoAdditionalData(context.DeletionResponse);
        Assert.Equal(Response_status.Ok, context.DeletionResponse?.Status);

        var infoResponse = await client.Groups.GetInfoJson.PostAsync(new() { Id = groupId }, null, TestContext.Current.CancellationToken);
        Assert.Null(infoResponse?.GroupResponse?.Group);
    }
}