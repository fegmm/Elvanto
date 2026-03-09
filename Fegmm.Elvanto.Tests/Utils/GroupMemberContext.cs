using Fegmm.Elvanto.Groups.AddPersonJson;
using Fegmm.Elvanto.Groups.RemovePersonJson;
using Fegmm.Elvanto.Models;

namespace Fegmm.Elvanto.Tests.Utils;

public class GroupMemberContext(ElvantoClient client, string groupId, string personId) : ResourceContext<(string GroupId, string PersonId), AddPersonPostRequestBody, GroupPersonModifiedResponse, GroupPersonModifiedResponse>
{
    protected override async Task<((string GroupId, string PersonId), GroupPersonModifiedResponse)> CreateResource(AddPersonPostRequestBody request)
    {
        var response =
            await client.Groups.AddPersonJson.PostAsync(request, null,
                TestContext.Current.CancellationToken);

        if (response?.ErrorResponse is not null)
        {
            var message = $"Failed to add person to group: {response.ErrorResponse.Error?.Code} - {response.ErrorResponse.Error?.Message}";
            if (SkipTestIfResourceCreationFails)
            {
                Assert.Skip(message);
            }
            else
            {
                Assert.Fail(message);
            }
        }

        return ((groupId, personId), response?.GroupPersonModifiedResponse!);
    }

    protected override async Task<GroupPersonModifiedResponse> CleanupResource()
    {
        var response = await client.Groups.RemovePersonJson.PostAsync(
            new RemovePersonPostRequestBody { Id = groupId, PersonId = personId }, null,
            TestContext.Current.CancellationToken);

        if (response?.GroupPersonModifiedResponse?.Status != Response_status.Ok)
        {
            throw new InvalidOperationException($"Failed to remove person from group: {response?.ErrorResponse?.Error?.Code} - {response?.ErrorResponse?.Error?.Message}");
        }

        return response.GroupPersonModifiedResponse;
    }
}
