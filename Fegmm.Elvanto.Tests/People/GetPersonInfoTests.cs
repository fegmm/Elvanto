using Fegmm.Elvanto.People.GetInfoJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.People;

public class GetPersonInfoTests : BaseTest
{
    [Fact]
    public async Task GetInfo_Works()
    {
        // First get a person
        var allPeople = await client.People.GetAllJson.PostAsync(new(), null, TestContext.Current.CancellationToken);
        Assert.NotNull(allPeople?.PeopleQueryResponse?.People?.Person);
        var person = allPeople.PeopleQueryResponse.People.Person.FirstOrDefault();
        if (person == null) return;

        GetInfoPostRequestBody body = new()
        {
            Id = person.Id,
            Fields = ["gender", "birthday"]
        };

        var response = await client.People.GetInfoJson.PostAsync(body, null, TestContext.Current.CancellationToken);

        Assert.NotNull(response?.PersonResponse?.Person);
        var returnedPerson = response.PersonResponse.Person.FirstOrDefault();
        Assert.NotNull(returnedPerson);
        Assert.Equal(person.Id, returnedPerson.Id);
        Assert.NoAdditionalData(returnedPerson);
    }
}
