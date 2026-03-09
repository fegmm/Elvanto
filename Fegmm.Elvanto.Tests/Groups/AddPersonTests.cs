using Fegmm.Elvanto.Groups.AddPersonJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;
using CreateGroupRequestBody = Fegmm.Elvanto.Groups.CreateJson.CreatePostRequestBody;
using CreatePersonRequestBody = Fegmm.Elvanto.People.CreateJson.CreatePostRequestBody;

namespace Fegmm.Elvanto.Tests.Groups;

public class AddPersonTests : BaseTest
{
    [Fact]
    public async Task AddPerson_To_Group_Works()
    {
        // Create a test group
        var createGroupRequest = new CreateGroupRequestBody
        {
            Name = $"API Test - {TestContext.Current.Test!.TestDisplayName}",
            Status = GroupState.Active
        };
        await using var groupContext = await new GroupContext(client).Create(createGroupRequest);
        var groupId = groupContext.Resource;
        Assert.False(string.IsNullOrEmpty(groupId));

        // Create a test person
        var createPersonRequest = new CreatePersonRequestBody
        {
            Firstname = "Test",
            Lastname = $"{TestContext.Current.Test!.TestDisplayName}",
        };
        await using var personContext = await new PersonContext(client).Create(createPersonRequest);
        var personId = personContext.Resource;
        Assert.False(string.IsNullOrEmpty(personId));

        // Add person to group
        var addPersonRequest = new AddPersonPostRequestBody
        {
            Id = groupId,
            PersonId = personId
        };
        await using var memberContext = await new GroupMemberContext(client, groupId, personId) { SkipTestIfResourceCreationFails = false }
            .Create(addPersonRequest);
        Assert.NotNull(memberContext.CreationResponse);
        Assert.NoAdditionalData(memberContext.CreationResponse);

        // Verify the person was added by fetching group info
        var infoResponse = await client.Groups.GetInfoJson.PostAsync(new() { Id = groupId }, null, TestContext.Current.CancellationToken);
        Assert.NotNull(infoResponse?.GroupResponse);
    }
}
