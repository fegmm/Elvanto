namespace Fegmm.Elvanto.Tests.People;

public class GetCurrentUserTests : BaseTest
{
    [Fact]
    public async Task etCurrentUser_FailsWithApiToken()
    {
        var response =
            await client.People.CurrentUserJson.PostAsync(null,
                TestContext.Current.CancellationToken);

        Assert.NotNull(response?.ErrorResponse?.Error);
        Assert.Equal(250, response.ErrorResponse.Error.Code);
        Assert.Equal("This method can only be called when you authenticate the API using OAuth.", response.ErrorResponse.Error.Message);
    }
}
