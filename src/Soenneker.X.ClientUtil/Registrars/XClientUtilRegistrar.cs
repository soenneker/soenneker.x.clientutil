using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.X.Client.Registrars;
using Soenneker.X.ClientUtil.Abstract;

namespace Soenneker.X.ClientUtil.Registrars;

/// <summary>
/// Registers the configured X OpenAPI client provider.
/// </summary>
public static class XClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IXClientUtil"/> as a singleton service.
    /// </summary>
    public static IServiceCollection AddXClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddXHttpClientAsSingleton().TryAddSingleton<IXClientUtil, XClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IXClientUtil"/> as a scoped service while keeping the underlying HTTP transport singleton.
    /// </summary>
    public static IServiceCollection AddXClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddXHttpClientAsSingleton().TryAddScoped<IXClientUtil, XClientUtil>();

        return services;
    }
}
