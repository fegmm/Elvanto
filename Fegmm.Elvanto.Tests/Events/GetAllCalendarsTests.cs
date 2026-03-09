
using Fegmm.Elvanto.Calendar.GetAllJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Events;

public class GetAllCalendarsTests : BaseTest
{
    [Fact]
    public async Task GetAll_Works()
    {
        var response = await client.Calendar.GetAllJson.PostAsync(null, TestContext.Current.CancellationToken);
        Assert.NotNull(response?.CalendarQueryResponse?.Calendars?.Calendar);
        Assert.NoAdditionalData(response.CalendarQueryResponse);
    }
}
