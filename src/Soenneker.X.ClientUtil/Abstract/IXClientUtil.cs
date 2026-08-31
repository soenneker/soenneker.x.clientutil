using System;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.X.OpenApiClient;

namespace Soenneker.X.ClientUtil.Abstract;

/// <summary>
/// Provides a lazily created X OpenAPI client that authenticates requests with one configured bearer token.
/// </summary>
public interface IXClientUtil : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets the cached X OpenAPI client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured X OpenAPI client.</returns>
    ValueTask<XOpenApiClient> Get(CancellationToken cancellationToken = default);
}
