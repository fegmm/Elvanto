namespace Fegmm.Elvanto;

public class ElvantoOptions
{
    /// <summary>
    /// The API Token used to authenticate against the Elvanto instance.
    /// </summary>
    public required string ApiToken { get; set; }

    /// <summary>
    /// The base URL of the Elvanto Api.
    /// </summary>
    public required string BaseUrl { get; set; } = "https://api.elvanto.com/v1";
}
