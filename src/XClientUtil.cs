using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.BearerAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.X.Client.Abstract;
using Soenneker.X.ClientUtil.Abstract;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.X.OpenApiClient;

namespace Soenneker.X.ClientUtil;

/// <inheritdoc cref="IXClientUtil"/>
public sealed class XClientUtil: IXClientUtil
{
    private readonly AsyncSingleton<XOpenApiClient> _client;
    public XClientUtil(IXHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<XOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("X:ApiKey");

            var requestAdapter = new HttpClientRequestAdapter(new BearerAuthenticationProvider(apiKey), httpClient: httpClient);

            return new XOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<XOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
