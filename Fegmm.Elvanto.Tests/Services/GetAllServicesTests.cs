using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Services.GetAllJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Services;

public class GetAllServicesTests : BaseTest
{
    [Fact]
    public async Task GetAll_AllAdditionalFields_Works()
    {
        GetAllPostRequestBody body = new()
        {
            Fields = [.. Enum.GetValues<ServiceAdditionalFields>()]
        };
        var response = await client.Services.GetAllJson.PostAsync(body, null,
            TestContext.Current.CancellationToken);

        Assert.NotNull(response?.ServiceQueryResponse?.Services?.Service);
        Assert.All(response.ServiceQueryResponse.Services.Service, Assert.NoAdditionalData);
    }
}
