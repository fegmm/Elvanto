using Fegmm.Elvanto.Calendar.Events.CreateJson;
using Fegmm.Elvanto.Calendar.Events.RemoveJson;
using Fegmm.Elvanto.Models;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.Events;

public class RemoveEventTests : BaseTest
{
    [Fact]
    public async Task Remove_Event_Works()
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
        await context.DisposeAsync();
    
        Assert.Equal(Response_status.Ok, context.DeletionResponse?.Status);
        Assert.NoAdditionalData(context.DeletionResponse);
    }
}
