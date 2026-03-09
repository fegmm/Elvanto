using Fegmm.Elvanto.People.GetAllJson;
using Fegmm.Elvanto.Tests.Utils;

namespace Fegmm.Elvanto.Tests.People;

public class GetAllPeopleTests : BaseTest
{
    [Fact]
    public async Task GetAll_AllAdditionalFields_Works()
    {
        GetAllPostRequestBody body = new()
        {
            Fields =
            [
                .. Enum.GetValues<Models.PeopleAdditionalQueryFields>().Cast<Models.PeopleAdditionalQueryFields?>()
            ]
        };
        var response =
            await client.People.GetAllJson.PostAsync(body, null,
                TestContext.Current.CancellationToken);

        Assert.NotNull(response?.PeopleQueryResponse?.People?.Person);
        Assert.NotEmpty(response.PeopleQueryResponse.People.Person);
        Assert.NoAdditionalData(response.PeopleQueryResponse);
    }
}
