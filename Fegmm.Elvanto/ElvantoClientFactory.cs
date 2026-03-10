using Microsoft.Extensions.Options;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Fegmm.Elvanto;

/// <summary>
/// Factory for creating <see cref="ElvantoClient"/> instances using an injected <see cref="HttpClient"/>.
/// </summary>
public class ElvantoClientFactory(HttpClient httpClient, IOptions<ElvantoOptions> options)
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly ElvantoOptions _options = options.Value ?? throw new ArgumentNullException(nameof(options));

    public ElvantoClient GetClient()
    {
        BasicAuthenticationProvider authProvider = new(_options.ApiToken, "-");
        HttpClientRequestAdapter httpClientRequestAdapter = new(authProvider, httpClient: _httpClient, serializationWriterFactory: new ElvantoSerializationWriterFactory(), parseNodeFactory: new ElvantoParseNodeFactory())
        {
            BaseUrl = _options.BaseUrl,
        };
        return new ElvantoClient(httpClientRequestAdapter);
    }
}
