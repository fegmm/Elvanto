namespace Fegmm.Elvanto.Tests;

public abstract class ResourceContext<TEntity, TRequest, TCreationResponse, TDeletionResponse> : IAsyncDisposable
{
    public bool SkipTestIfResourceCreationFails { get; set; } = true;

    public TEntity Resource { get; protected set; } = default!;
    public TCreationResponse? CreationResponse { get; protected set; }
    public TDeletionResponse? DeletionResponse { get; protected set; }

    public async ValueTask DisposeAsync()
    {
        if (Resource is not null)
        {
            DeletionResponse = await CleanupResource();
            Resource = default!;
        }
    }

    public async Task<ResourceContext<TEntity, TRequest, TCreationResponse, TDeletionResponse>> Create(TRequest request)
    {
        try
        {
            (Resource, CreationResponse) = await CreateResource(request);
        }
        catch (Exception ex)
        {
            if (SkipTestIfResourceCreationFails)
            {
                Assert.Skip($"Failed to create resource: {ex.Message}");
            }
            else
            {
                throw;
            }
        }
        return this;
    }

    protected abstract Task<(TEntity, TCreationResponse)> CreateResource(TRequest request);

    protected abstract Task<TDeletionResponse> CleanupResource();
}