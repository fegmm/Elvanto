using Fegmm.Elvanto.Calendar.Events.CreateJson;
using Fegmm.Elvanto.Models;

namespace Fegmm.Elvanto.Tests.Utils;

public class EventContext(ElvantoClient client, string? calendarId = null, string? organizerId = null) : ResourceContext<string, CreatePostRequestBody, EventModifiedResponse, EventDeleteResponse>
{
    private IAsyncDisposable? _personContext;

    protected override async Task<(string, EventModifiedResponse)> CreateResource(CreatePostRequestBody request)
    {
        if (calendarId == null)
        {
            calendarId = (await client.Calendar.GetAllJson.PostAsync(null, TestContext.Current.CancellationToken))?
                .CalendarQueryResponse?
                .Calendars?
                .Calendar?
                .OrderBy(c => c.Published)
                .ThenBy(c => c.Members)
                .FirstOrDefault()?
                .Id;
            
            Assert.SkipWhen(calendarId == null, "Could not find a calendar id to create an event in!");
        }

        if (organizerId == null)
        {
            var personContext = await new PersonContext(client).Create(new Elvanto.People.CreateJson.CreatePostRequestBody
            {
                Firstname = "Event Organizer",
                Lastname = $"{TestContext.Current.Test!.TestDisplayName}"
            });
            _personContext = personContext;
            organizerId = personContext.Resource;
        }

        request.Organizer = organizerId;
        request.CalendarId = calendarId;

        var response = await client.Calendar.Events.CreateJson.PostAsync(request, null, TestContext.Current.CancellationToken);

        if (response?.ErrorResponse is not null)
        {
            var message =
                $"Failed to create event: {response.ErrorResponse.Error?.Code} - {response.ErrorResponse.Error?.Message}";
            if (SkipTestIfResourceCreationFails)
            {
                Assert.Skip(message);
            }
            else
            {
                Assert.Fail(message);
            }
        }

        return (response?.EventModifiedResponse?.Event?.Id!, response?.EventModifiedResponse!);
    }

    protected override async Task<EventDeleteResponse> CleanupResource()
    {
        var response = await client.Calendar.Events.RemoveJson.PostAsync(new() { Id = Resource }, null, TestContext.Current.CancellationToken);

        if (response?.EventDeleteResponse?.Status != Response_status.Ok)
        {
            throw new InvalidOperationException($"Failed to delete event: {response?.ErrorResponse?.Error?.Code} - {response?.ErrorResponse?.Error?.Message}");
        }

        if (_personContext != null)
        {
            await _personContext.DisposeAsync();
        }

        return response.EventDeleteResponse;
    }
}
