using Fegmm.Elvanto.Calendar.Events.EditJson;
using Fegmm.Elvanto.Calendar.Events.CreateJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Events;

public class EditEventTests : BaseTest
{
    [Fact]
    public async Task Edit_Event_Works()
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
        await using var context = await new EventContext(client).Create(request);
        var eventId = context.Resource;
        Assert.False(string.IsNullOrEmpty(eventId));

        // Edit the event
        var editRequest = new EditPostRequestBody
        {
            Id = eventId,
            Name = $"{request.Name} - Edited"
        };
        var editResponse = await client.Calendar.Events.EditJson.PostAsync(editRequest, null,
            TestContext.Current.CancellationToken);

        Assert.NotNull(editResponse);
        Assert.Equal(Response_status.Ok, editResponse?.EventModifiedResponse?.Status);
    }
}
