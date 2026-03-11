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
                                Models.PersonAdditionalFields.Gender,
                Models.PersonAdditionalFields.Birthday,
                Models.PersonAdditionalFields.Anniversary,
                Models.PersonAdditionalFields.School_grade,
                Models.PersonAdditionalFields.Marital_status,
                Models.PersonAdditionalFields.Development_child,
                Models.PersonAdditionalFields.Special_needs_child,
                Models.PersonAdditionalFields.Security_code,
                Models.PersonAdditionalFields.Receipt_name,
                Models.PersonAdditionalFields.Giving_number,
                Models.PersonAdditionalFields.Mailing_address,
                Models.PersonAdditionalFields.Mailing_address2,
                Models.PersonAdditionalFields.Mailing_city,
                Models.PersonAdditionalFields.Mailing_state,
                Models.PersonAdditionalFields.Mailing_postcode,
                Models.PersonAdditionalFields.Mailing_country,
                Models.PersonAdditionalFields.Home_address,
                Models.PersonAdditionalFields.Home_address2,
                Models.PersonAdditionalFields.Home_city,
                Models.PersonAdditionalFields.Home_state,
                Models.PersonAdditionalFields.Home_postcode,
                Models.PersonAdditionalFields.Home_country,
                Models.PersonAdditionalFields.Access_permissions,
                Models.PersonAdditionalFields.Departments,
                Models.PersonAdditionalFields.Service_types,
                Models.PersonAdditionalFields.Demographics,
                Models.PersonAdditionalFields.Locations,
                Models.PersonAdditionalFields.Family,
                Models.PersonAdditionalFields.Reports_to,
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
