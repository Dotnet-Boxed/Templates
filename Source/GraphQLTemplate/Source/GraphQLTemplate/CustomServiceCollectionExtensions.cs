namespace GraphQLTemplate;

using Boxed.AspNetCore;
using GraphQLTemplate.ConfigureOptions;
#if (Authorization || CORS || OpenTelemetry)
using GraphQLTemplate.Constants;
#endif
using GraphQLTemplate.Options;
using HotChocolate.Execution.Options;
#if (!ForwardedHeaders && HostFiltering)
using Microsoft.AspNetCore.HostFiltering;
#endif
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
#if OpenTelemetry
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
#endif
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
#if CORS
            .ConfigureOptions<ConfigureCorsOptions>()
#endif
#if HstsPreload
            .ConfigureOptions<ConfigureHstsOptions>()
#endif
#if Redis
            .ConfigureOptions<ConfigureRedisCacheOptions>()
#endif
#if ResponseCompression
            .ConfigureOptions<ConfigureResponseCompressionOptions>()
#endif
            .ConfigureOptions<ConfigureRouteOptions>();
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
#if OpenTelemetry

    /// <summary>
    /// Adds Open Telemetry services and configures instrumentation and exporters.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="webHostEnvironment">The environment the application is running under.</param>
    /// <returns>The services with open telemetry added.</returns>
    public static IServiceCollection AddCustomOpenTelemetryTracing(this IServiceCollection services, IWebHostEnvironment webHostEnvironment) =>
        services.AddOpenTelemetryTracing(
            builder =>
            {
                builder
                    .SetResourceBuilder(ResourceBuilder
                        .CreateEmpty()
                        .AddService(webHostEnvironment.ApplicationName, serviceVersion: AssemblyInformation.Current.Version)
                        .AddAttributes(
                            new KeyValuePair<string, object>[]
                            {
                                new(OpenTelemetryAttributeName.Deployment.Environment, webHostEnvironment.EnvironmentName),
                                new(OpenTelemetryAttributeName.Host.Name, Environment.MachineName),
                            })
                        .AddEnvironmentVariableDetector())
                    .AddAspNetCoreInstrumentation(
                        options =>
                        {
                            // Enrich spans with additional request and response meta data.
                            // See https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md
                            options.Enrich = (activity, eventName, obj) =>
                        {
                            if (obj is HttpRequest request)
                            {
                                var context = request.HttpContext;
                                activity.AddTag(OpenTelemetryAttributeName.Http.Flavor, GetHttpFlavour(request.Protocol));
                                activity.AddTag(OpenTelemetryAttributeName.Http.Scheme, request.Scheme);
                                activity.AddTag(OpenTelemetryAttributeName.Http.ClientIP, context.Connection.RemoteIpAddress);
                                activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentLength, request.ContentLength);
                                activity.AddTag(OpenTelemetryAttributeName.Http.RequestContentType, request.ContentType);

                                var user = context.User;
                                if (user.Identity?.Name is not null)
                                {
                                    activity.AddTag(OpenTelemetryAttributeName.EndUser.Id, user.Identity.Name);
                                    activity.AddTag(OpenTelemetryAttributeName.EndUser.Scope, string.Join(',', user.Claims.Select(x => x.Value)));
                                }
                            }
                            else if (obj is HttpResponse response)
                            {
                                activity.AddTag(OpenTelemetryAttributeName.Http.ResponseContentLength, response.ContentLength);
                                activity.AddTag(OpenTelemetryAttributeName.Http.ResponseContentType, response.ContentType);
                            }

                            static string GetHttpFlavour(string protocol)
                            {
                                if (HttpProtocol.IsHttp10(protocol))
                                {
                                    return OpenTelemetryHttpFlavour.Http10;
                                }
                                else if (HttpProtocol.IsHttp11(protocol))
                                {
                                    return OpenTelemetryHttpFlavour.Http11;
                                }
                                else if (HttpProtocol.IsHttp2(protocol))
                                {
                                    return OpenTelemetryHttpFlavour.Http20;
                                }
                                else if (HttpProtocol.IsHttp3(protocol))
                                {
                                    return OpenTelemetryHttpFlavour.Http30;
                                }

                                throw new InvalidOperationException($"Protocol {protocol} not recognised.");
                            }
                        };
                            options.RecordException = true;
                        });
#if Redis
                builder.AddRedisInstrumentation();
#endif

                if (webHostEnvironment.IsDevelopment())
                {
                    builder.AddConsoleExporter(
                        options => options.Targets = ConsoleExporterOutputTargets.Console | ConsoleExporterOutputTargets.Debug);
                }

                // TODO: Add OpenTelemetry.Instrumentation.* NuGet packages and configure them to collect more span data.
                //       E.g. Add the OpenTelemetry.Instrumentation.Http package to instrument calls to HttpClient.
                // TODO: Add OpenTelemetry.Exporter.* NuGet packages and configure them here to export open telemetry span data.
                //       E.g. Add the OpenTelemetry.Exporter.OpenTelemetryProtocol package to export span data to Jaeger.
            });
#endif
#if Authorization

    /// <summary>
    /// Add GraphQL authorization (See https://github.com/graphql-dotnet/authorization).
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>The services with caching services added.</returns>
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services) =>
        services
            .AddAuthorization(options => options
                .AddPolicy(AuthorizationPolicyName.Admin, x => x.RequireAuthenticatedUser()));
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
