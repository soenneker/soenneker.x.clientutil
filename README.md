[![](https://img.shields.io/nuget/v/soenneker.x.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.x.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.x.clientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.x.clientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.x.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.x.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.x.clientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.x.clientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.X.ClientUtil

Provides a lazily created, cached `XOpenApiClient` that authenticates every request with one configured bearer token.

## Installation

```shell
dotnet add package Soenneker.X.ClientUtil
```

## Configuration

```json
{
  "X": {
    "BearerToken": "your-app-bearer-token"
  }
}
```

`X:ApiKey` remains supported as a legacy configuration key.

## Registration

Register one client for the application:

```csharp
services.AddXClientUtilAsSingleton();
```

Or register the OpenAPI client provider per scope while retaining the singleton HTTP transport:

```csharp
services.AddXClientUtilAsScoped();
```

## Usage

```csharp
public sealed class XSearchService
{
    private readonly IXClientUtil _xClientUtil;

    public XSearchService(IXClientUtil xClientUtil)
    {
        _xClientUtil = xClientUtil;
    }

    public async Task SearchUsers(CancellationToken cancellationToken)
    {
        XOpenApiClient client = await _xClientUtil.Get(cancellationToken);

        var response = await client.Two.Users.Search.GetAsync(request =>
        {
            request.QueryParameters.Query = "dotnet";
        }, cancellationToken);

        foreach (var user in response?.Data ?? [])
            Console.WriteLine($"{user.Name} (@{user.Username})");
    }
}
```

This package captures one bearer token when the provider is created, so it fits app-only access and other endpoints that accept that token. For requests made on behalf of different users, use `Soenneker.X.Client` and attach the appropriate OAuth token to each request instead of sharing it through this client.

Disposing a scoped `IXClientUtil` releases its generated client; it does not remove the singleton HTTP transport.
