namespace GraphQLTemplate;

using Boxed.AspNetCore;
using GraphQLTemplate.ConfigureOptions;
#if ((HealthCheck && Redis) || Redis || PersistedQueries || Subscriptions)
using GraphQLTemplate.Constants;
#endif
using GraphQLTemplate.Options;
using HotChocolate.Execution.Options;
#if (!ForwardedHeaders && HostFiltering)
using Microsoft.AspNetCore.HostFiltering;
#endif
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
#if Redis
using StackExchange.Redis;
#endif

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
    /// <returns>The services with caching services added.</returns>
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
            .ConfigureAndValidateSingleton<GraphQLOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.GraphQL)))
            .ConfigureAndValidateSingleton<RequestExecutorOptions>(
                configuration.GetRequiredSection(nameof(ApplicationOptions.GraphQL)).GetRequiredSection(nameof(GraphQLOptions.Request)))
#if Redis
            .ConfigureAndValidateSingleton<RedisOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Redis)))
#endif
            .ConfigureAndValidateSingleton<HostOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Host)))
            .ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetRequiredSection(nameof(ApplicationOptions.Kestrel)));

    public static IServiceCollection AddCustomConfigureOptions(this IServiceCollection services) =>
        services
#if Authorization
            .ConfigureOptions<ConfigureAuthorizationOptions>()
#endif
#if CORS
            .ConfigureOptions<ConfigureCorsOptions>()
#endif
#if HstsPreload
            .ConfigureOptions<ConfigureHstsOptions>()
#endif
#if DistributedCacheRedis
            .ConfigureOptions<ConfigureRedisCacheOptions>()
#endif
#if Serilog
            .ConfigureOptions<ConfigureRequestLoggingOptions>()
#endif
#if ResponseCompression
            .ConfigureOptions<ConfigureResponseCompressionOptions>()
#endif
            .ConfigureOptions<ConfigureRouteOptions>()
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
#if Redis

    public static IServiceCollection AddCustomRedis(
        this IServiceCollection services,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration) =>
        services.AddIf(
            !webHostEnvironment.IsEnvironment(EnvironmentName.Test),
            x => x.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(
                     configuration
                        .GetRequiredSection(nameof(ApplicationOptions.Redis))
                        .Get<RedisOptions>()
                        .ConfigurationOptions)));
#endif

    public static IServiceCollection AddCustomGraphQL(
        this IServiceCollection services,
        IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration)
    {
        var graphQLOptions = configuration.GetRequiredSection(nameof(ApplicationOptions.GraphQL)).Get<GraphQLOptions>();
        return services
            .AddGraphQLServer()
#if OpenTelemetry
            .AddInstrumentation()
#endif
            .InitializeOnStartup()
            .AddFiltering()
            .AddSorting()
            .AddGlobalObjectIdentification()
            .AddQueryFieldToMutationPayloads()
            .AddApolloTracing()
#if Authorization
            .AddAuthorization()
#endif
#if PersistedQueries
            .UseAutomaticPersistedQueryPipeline()
            .AddIfElse(
                webHostEnvironment.IsEnvironment(EnvironmentName.Test),
                x => x.AddInMemoryQueryStorage(),
                x => x.AddRedisQueryStorage())
#endif
#if Subscriptions
            .AddIfElse(
                webHostEnvironment.IsEnvironment(EnvironmentName.Test),
                x => x.AddInMemorySubscriptions(),
                x => x.AddRedisSubscriptions())
#endif
            .AddProjectScalarTypes()
            .AddProjectDirectives()
            .AddProjectDataLoaders()
            .AddProjectTypes()
            .TrimTypes()
            .ModifyOptions(options => options.UseXmlDocumentation = false)
            .AddMaxExecutionDepthRule(graphQLOptions.MaxAllowedExecutionDepth)
            .SetPagingOptions(graphQLOptions.Paging)
            .SetRequestOptions(() => graphQLOptions.Request)
            .Services;
    }
}
