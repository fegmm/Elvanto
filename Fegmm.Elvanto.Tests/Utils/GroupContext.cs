using Fegmm.Elvanto.Groups.CreateJson;
using Fegmm.Elvanto.Models;

namespace Fegmm.Elvanto.Tests.Utils;

public class GroupContext(ElvantoClient client) : ResourceContext<string, CreatePostRequestBody, GroupModifiedResponse, GroupModifiedResponse>
{
    protected override async Task<(string, GroupModifiedResponse)> CreateResource(CreatePostRequestBody request)
    {
        var response =
            await client.Groups.CreateJson.PostAsync(request, null,
                TestContext.Current.CancellationToken);

        if (response?.ErrorResponse is not null)
        {
            var message = $"Failed to create group: {response.ErrorResponse.Error?.Code} - {response.ErrorResponse.Error?.Message}";
            if (SkipTestIfResourceCreationFails)
            {
                Assert.Skip(message);
            }
            else
            {
                Assert.Fail(message);
            }
        }

        return (response?.GroupModifiedResponse?.Group?.Id!, response?.GroupModifiedResponse!);
    }

    protected override async Task<GroupModifiedResponse> CleanupResource()
    {
        var response = await client.Groups.RemoveJson.PostAsync(new() { Id = Resource }, null,
            TestContext.Current.CancellationToken);

        if (response?.GroupModifiedResponse?.Status != Response_status.Ok)
        {
            throw new InvalidOperationException($"Failed to delete group: {response?.ErrorResponse?.Error?.Code} - {response?.ErrorResponse?.Error?.Message}");
        }

        return response.GroupModifiedResponse;
    }
}


