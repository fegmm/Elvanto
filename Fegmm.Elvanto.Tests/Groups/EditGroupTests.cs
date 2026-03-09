using Fegmm.Elvanto.Groups.CreateJson;
using Fegmm.Elvanto.Groups.EditJson;
using Fegmm.Elvanto.Groups.GetInfoJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Groups;

public class EditGroupTests : BaseTest
{
    [Fact]
    public async Task Edit_Group_Works()
    {
        var createRequest = new CreatePostRequestBody
        {
            Name = $"API Test - {TestContext.Current.Test!.TestDisplayName}",
            Status = GroupState.Active
        };
        await using var context = await new GroupContext(client).Create(createRequest);
        var groupId = context.Resource;

        string newName = createRequest.Name + " Edited";
        var editRequest = new EditPostRequestBody
        {
            Id = groupId,
            Name = newName
        };
        var editResponse = await client.Groups.EditJson.PostAsync(editRequest, null,
            TestContext.Current.CancellationToken);
        Assert.NotNull(editResponse?.GroupModifiedResponse?.Group);
        Assert.Equal(groupId, editResponse.GroupModifiedResponse.Group.Id);

        // Verify edit
        var infoResponse = await client.Groups.GetInfoJson.PostAsync(new() { Id = groupId },
            null, TestContext.Current.CancellationToken);
        Assert.Equal(newName, infoResponse?.GroupResponse?.Group?.Single()?.Name);
    }
}