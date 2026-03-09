using Fegmm.Elvanto.People.SearchJson;
using Fegmm.Elvanto.Tests.Utils;
using CreatePersonRequestBody = Fegmm.Elvanto.People.CreateJson.CreatePostRequestBody;

namespace Fegmm.Elvanto.Tests.People;

public class SearchPeopleTests : BaseTest
{
    [Fact]
    public async Task Search_ByName_Works()
    {
        // First create a person to search for
        var createPersonRequest = new CreatePersonRequestBody
        {
            Firstname = "Test",
            Lastname = $"{TestContext.Current.Test!.TestDisplayName}"
        };
        await using var personContext = await new PersonContext(client).Create(createPersonRequest);
        var personId = personContext.Resource;
        Assert.False(string.IsNullOrEmpty(personId));

        var allPeople = await client.People.GetAllJson.PostAsync(new(), null, TestContext.Current.CancellationToken);
        Assert.NotNull(allPeople?.PeopleQueryResponse?.People?.Person);
        var person = allPeople.PeopleQueryResponse.People.Person.FirstOrDefault();
        if (person == null) return;

        SearchPostRequestBody body = new()
        {
            Search = new SearchPostRequestBody_search
            {
                Firstname = person.Firstname,
                Lastname = person.Lastname
            }
        };

        var response = await client.People.SearchJson.PostAsync(body, null, TestContext.Current.CancellationToken);

        Assert.NotNull(response?.PeopleQueryResponse?.People?.Person);
        Assert.Contains(response.PeopleQueryResponse.People.Person, p => p.Id == person.Id);
        Assert.All(response.PeopleQueryResponse.People.Person, Assert.NoAdditionalData);
    }
}
