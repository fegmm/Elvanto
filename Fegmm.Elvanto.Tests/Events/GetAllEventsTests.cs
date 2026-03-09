using Fegmm.Elvanto.Calendar.Events.GetAllJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Events;

public class GetAllEventsTests : BaseTest
{
    [Fact]
    public async Task GetAll_AllAdditionalFields_Works()
    {
        GetAllPostRequestBody body = new()
        {
            Start = DateOnly.FromDateTime(DateTime.UtcNow),
            End = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(30)),
            Fields = [.. Enum.GetValues<GetAllPostRequestBody_fields>()]
        };
        var response = await client.Calendar.Events.GetAllJson.PostAsync(body, null,
            TestContext.Current.CancellationToken);

        Assert.NotNull(response?.EventQueryResponse?.Events?.Event);
        Assert.NoAdditionalData(response.EventQueryResponse);
    }
}
