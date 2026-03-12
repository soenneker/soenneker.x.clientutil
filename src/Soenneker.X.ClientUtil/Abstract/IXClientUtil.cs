using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.X.OpenApiClient;

namespace Soenneker.X.ClientUtil.Abstract;

/// <summary>
/// An async thread-safe singleton for X OpenApiClient
/// </summary>
public interface IXClientUtil : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets a configured X OpenAPI client instance
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A configured X OpenAPI client</returns>
    ValueTask<XOpenApiClient> Get(CancellationToken cancellationToken = default);
}