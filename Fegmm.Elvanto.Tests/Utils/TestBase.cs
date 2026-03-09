using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fegmm.Elvanto.Tests;

public class BaseTest
{
    protected ElvantoClient client;

    public BaseTest()
    {
        var apiKey = new ConfigurationBuilder()
                         .AddEnvironmentVariables()
                         .AddUserSecrets<BaseTest>()
                         .Build()["ApiKey"] ??
                     throw new ArgumentNullException("ApiKey must be set in user secrets or as environment variable");

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddElvantoClient((options, _) => options.ApiToken = apiKey);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        client = serviceProvider.GetRequiredService<ElvantoClient>();
    }
}