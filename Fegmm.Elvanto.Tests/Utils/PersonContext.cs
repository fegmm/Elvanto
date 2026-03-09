using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.People.CreateJson;
using Fegmm.Elvanto.People.RemoveJson;

namespace Fegmm.Elvanto.Tests.Utils;

public class PersonContext(ElvantoClient client) : ResourceContext<string, CreatePostRequestBody, PersonUpsertedResponse, DeletePersonResponse>
{
    protected override async Task<(string, PersonUpsertedResponse)> CreateResource(CreatePostRequestBody request)
    {
        var response =
            await client.People.CreateJson.PostAsync(request, null,
                TestContext.Current.CancellationToken);

        if (response?.ErrorResponse is not null)
        {
            var message = $"Failed to create person: {response.ErrorResponse.Error?.Code} - {response.ErrorResponse.Error?.Message}";
            if (SkipTestIfResourceCreationFails)
            {
                Assert.Skip(message);
            }
            else
            {
                Assert.Fail(message);
            }
        }

        return (response?.PersonUpsertedResponse?.Person?.Id!, response?.PersonUpsertedResponse!);
    }

    protected override async Task<DeletePersonResponse> CleanupResource()
    {
        var response = await client.People.RemoveJson.PostAsync(new RemovePostRequestBody { Id = Resource }, null,
            TestContext.Current.CancellationToken);

        if (response?.DeletePersonResponse?.Status != Response_status.Ok)
        {
            throw new InvalidOperationException($"Failed to delete person: {response?.ErrorResponse?.Error?.Code} - {response?.ErrorResponse?.Error?.Message}");
        }
        return response.DeletePersonResponse;
    }
}
