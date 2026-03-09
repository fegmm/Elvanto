using Fegmm.Elvanto.Services.GetInfoJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Services;

public class GetServiceInfoTests : BaseTest
{
    [Fact]
    public async Task GetInfo_Works()
    {
        // First get a service
        var allServices = await client.Services.GetAllJson.PostAsync(new() { PageSize = 10 }, null, TestContext.Current.CancellationToken);
        Assert.SkipWhen(allServices?.ServiceQueryResponse?.Services?.Service is null, "Failed to fetch services");
        var service = allServices.ServiceQueryResponse.Services.Service.FirstOrDefault();
        if (service == null)
        {
            return;
        }

        GetInfoPostRequestBody body = new()
        {
            Id = service.Id,
            Fields = [.. Enum.GetValues<GetInfoPostRequestBody_fields>()]
        };

        var response = await client.Services.GetInfoJson.PostAsync(body, null, TestContext.Current.CancellationToken);

        var retrievedService = Assert.Single(response?.ServiceResponse?.Service ?? []);
        Assert.Equal(service.Id, retrievedService.Id);
        Assert.NoAdditionalData(retrievedService);
    }
}
