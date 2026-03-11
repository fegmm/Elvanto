using Fegmm.Elvanto.Calendar.Events.CreateJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Events;

public class CreateEventTests : BaseTest
{
    [Fact]
    public async Task Create_Event_Works()
    {
        var request = new CreatePostRequestBody
        {
            Name = $"API Test - {TestContext.Current.Test!.TestDisplayName}",
            Status = EventStatus.Draft,
            Start = DateTimeOffset.UtcNow.AddDays(1),
            End = DateTimeOffset.UtcNow.AddDays(1).AddHours(1),
            Description = "Test event description",
            AllDay = BooleanStringEnum.No
        };
        await using var context = await new EventContext(client) { SkipTestIfResourceCreationFails = false }.Create(request);
        // make sure the server didn't return any additional fields
        Assert.NotNull(context.CreationResponse);
        Assert.NoAdditionalData(context.CreationResponse);
        var eventId = context.Resource;
        Assert.False(string.IsNullOrEmpty(eventId));
    }
}
