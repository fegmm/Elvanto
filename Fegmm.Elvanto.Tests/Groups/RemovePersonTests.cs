using Fegmm.Elvanto.Groups.CreateJson;
using Fegmm.Elvanto.People.CreateJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;
using CreateGroupRequestBody = Fegmm.Elvanto.Groups.CreateJson.CreatePostRequestBody;
using CreatePersonRequestBody = Fegmm.Elvanto.People.CreateJson.CreatePostRequestBody;
using Fegmm.Elvanto.Groups.AddPersonJson;


namespace Fegmm.Elvanto.Tests.Groups;

public class RemovePersonTests : BaseTest
{
    [Fact]
    public async Task RemovePerson_From_Group_Works()
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

        // Add person to group first
        var addPersonRequest = new AddPersonPostRequestBody
        {
            Id = groupId,
            PersonId = personId
        };
        await using var memberContext = await new GroupMemberContext(client, groupId, personId)
            .Create(addPersonRequest);
        await memberContext.DisposeAsync();

        Assert.NoAdditionalData(memberContext.DeletionResponse);
        Assert.Equal(Response_status.Ok, memberContext.DeletionResponse?.Status);
    }
}
