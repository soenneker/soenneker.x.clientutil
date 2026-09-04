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

/// <inheritdoc cref="IXClientUtil" />
public sealed class XClientUtil : IXClientUtil
{
    private const string ApiHost = "api.x.com";

    private readonly IXHttpClient _httpClientUtil;
    private readonly string _bearerToken;

    private readonly AsyncSingleton<XOpenApiClient> _client;

    public XClientUtil(IXHttpClient httpClientUtil, IConfiguration configuration)
    {
        _httpClientUtil = httpClientUtil;
        _bearerToken = configuration["X:BearerToken"] ?? configuration.GetValueStrict<string>("X:ApiKey");

        // No closure: method group
        _client = new AsyncSingleton<XOpenApiClient>(CreateClient);
    }

    private async ValueTask<XOpenApiClient> CreateClient(CancellationToken token)
    {
        HttpClient httpClient = await _httpClientUtil.Get(token)
                                                     .NoSync();

        var requestAdapter = new HttpClientRequestAdapter(new BearerAuthenticationProvider(_bearerToken, ApiHost), httpClient: httpClient);

        return new XOpenApiClient(requestAdapter);
    }

    public ValueTask<XOpenApiClient> Get(CancellationToken cancellationToken = default) =>
        _client.Get(cancellationToken);

    public void Dispose() => _client.Dispose();

    public ValueTask DisposeAsync() => _client.DisposeAsync();
}
