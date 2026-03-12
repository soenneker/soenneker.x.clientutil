using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.X.Client.Registrars;
using Soenneker.X.ClientUtil.Abstract;

namespace Soenneker.X.ClientUtil.Registrars;

/// <summary>
/// An async thread-safe singleton for X OpenApiClient
/// </summary>
public static class XClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IXClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddXClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddXHttpClientAsSingleton().TryAddSingleton<IXClientUtil, XClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IXClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddXClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddXHttpClientAsSingleton().TryAddScoped<IXClientUtil, XClientUtil>();

        return services;
    }
}