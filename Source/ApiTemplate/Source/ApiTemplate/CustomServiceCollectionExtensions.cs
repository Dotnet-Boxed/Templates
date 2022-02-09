namespace ApiTemplate;

using ApiTemplate.ConfigureOptions;
#if (HealthCheck && Redis)
using ApiTemplate.Constants;
#endif
using ApiTemplate.Options;
using Boxed.AspNetCore;
#if (!ForwardedHeaders && HostFiltering)
using Microsoft.AspNetCore.HostFiltering;
#endif
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

/// <summary>
/// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
/// </summary>
internal static class CustomServiceCollectionExtensions
{
    /// <summary>
    /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
    /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services with options services added.</returns>
    public static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            // ConfigureAndValidateSingleton registers IOptions<T> and also T as a singleton to the services collection.
            .ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
            .ConfigureAndValidateSingleton<CacheProfileOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.CacheProfiles)))
#if ResponseCompression
            .ConfigureAndValidateSingleton<CompressionOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Compression)))
#endif
#if ForwardedHeaders
            .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.ForwardedHeaders)))
            .Configure<ForwardedHeadersOptions>(
                options =>
                {
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                })
#elif HostFiltering
            .ConfigureAndValidateSingleton<HostFilteringOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.HostFiltering)))
#endif
            .ConfigureAndValidateSingleton<HostOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Host)))
#if Redis
            .ConfigureAndValidateSingleton<RedisOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Redis)))
#endif
            .ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Kestrel)));

    public static IServiceCollection AddCustomConfigureOptions(this IServiceCollection services) =>
        services
#if Versioning
            .ConfigureOptions<ConfigureApiVersioningOptions>()
#endif
#if Controllers
            .ConfigureOptions<ConfigureMvcOptions>()
#endif
#if CORS
            .ConfigureOptions<ConfigureCorsOptions>()
#endif
#if HstsPreload
            .ConfigureOptions<ConfigureHstsOptions>()
#endif
            .ConfigureOptions<ConfigureJsonOptions>()
#if Redis
            .ConfigureOptions<ConfigureRedisCacheOptions>()
#endif
#if Serilog
            .ConfigureOptions<ConfigureRequestLoggingOptions>()
#endif
#if ResponseCompression
            .ConfigureOptions<ConfigureResponseCompressionOptions>()
#endif
            .ConfigureOptions<ConfigureRouteOptions>()
#if Swagger
            .ConfigureOptions<ConfigureSwaggerGenOptions>()
            .ConfigureOptions<ConfigureSwaggerUIOptions>()
#endif
            .ConfigureOptions<ConfigureStaticFileOptions>();
#if HealthCheck

    public static IServiceCollection AddCustomHealthChecks(
        this IServiceCollection services,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration) =>
        services
            .AddHealthChecks()
            // Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
#if Redis
            .AddIf(
                !webHostEnvironment.IsEnvironment(EnvironmentName.Test),
                x => x.AddRedis(configuration.GetRequiredSection(nameof(ApplicationOptions.Redis)).Get<RedisOptions>().ConfigurationOptions.ToString()))
#endif
            .Services;
#endif
}
