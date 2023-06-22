namespace OrleansTemplate.Server;

using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
#if ApplicationInsights
using Microsoft.ApplicationInsights.Extensibility;
#endif
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Serialization;
using OrleansTemplate.Abstractions.Constants;
using OrleansTemplate.Server.Options;
#if Serilog
using Serilog;
using Serilog.Extensions.Hosting;
#endif

public class Program
{
    public static async Task<int> Main(string[] args)
    {
#if Serilog
        Log.Logger = CreateBootstrapLogger();
#endif
        IHost? host = null;

        try
        {
#if Serilog
            Log.Information("Initialising.");
#endif
            host = CreateHostBuilder(args).Build();

            host.LogApplicationStarted();
            await host.RunAsync().ConfigureAwait(false);
            host.LogApplicationStopped();

            return 0;
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            host!.LogApplicationTerminatedUnexpectedly(exception);

            return 1;
        }
#if Serilog
        finally
        {
            await Log.CloseAndFlushAsync().ConfigureAwait(false);
        }
#endif
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        new HostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureHostConfiguration(
                configurationBuilder => configurationBuilder.AddCustomBootstrapConfiguration(args))
            .ConfigureAppConfiguration(
                (hostingContext, configurationBuilder) =>
                {
                    hostingContext.HostingEnvironment.ApplicationName = AssemblyInformation.Current.Product;
                    configurationBuilder.AddCustomConfiguration(hostingContext.HostingEnvironment, args);
                })
#if Serilog
            .UseSerilog(ConfigureReloadableLogger)
#endif
            .UseDefaultServiceProvider(
                (context, options) =>
                {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = isDevelopment;
                    options.ValidateOnBuild = isDevelopment;
                })
            .UseOrleans(ConfigureSiloBuilder)
#if HealthCheck
            .ConfigureWebHost(ConfigureWebHostBuilder)
#endif
            .UseConsoleLifetime();

    private static void ConfigureSiloBuilder(
        HostBuilderContext context,
        ISiloBuilder siloBuilder) =>
        siloBuilder
            .ConfigureServices(services => ConfigureServices(context, services))
            .UseAzureStorageClustering(
                options => options.ConfigureTableServiceClient(GetStorageOptions(context.Configuration).ConnectionString))
            .ConfigureEndpoints(
                EndpointOptions.DEFAULT_SILO_PORT,
                EndpointOptions.DEFAULT_GATEWAY_PORT,
                listenOnAnyHostAddress: !context.HostingEnvironment.IsDevelopment())
            .AddAzureTableGrainStorageAsDefault(
                options => options.ConfigureTableServiceClient(GetStorageOptions(context.Configuration).ConnectionString))
            .UseAzureTableReminderService(
                options => options.ConfigureTableServiceClient(GetStorageOptions(context.Configuration).ConnectionString))
            .UseTransactions()
            .AddAzureTableTransactionalStateStorageAsDefault(
                options => options.ConfigureTableServiceClient(GetStorageOptions(context.Configuration).ConnectionString))
            .AddAzureQueueStreams(
                StreamProviderName.Default,
                (SiloAzureQueueStreamConfigurator configurator) =>
                {
                    var queueOptions = GetQueueOptions(context.Configuration);

                    configurator.ConfigureAzureQueue(
                        x => x.Configure(options =>
                        {
                            options.ConfigureQueueServiceClient(queueOptions.ConnectionString);
                            options.QueueNames = queueOptions.QueueNames;
                        }));
                    configurator.ConfigureCacheSize(queueOptions.CacheSize);
                    configurator.ConfigurePullingAgent(
                        x => x.Configure(
                            options => options.GetQueueMsgsTimerPeriod = queueOptions.TimerPeriod));
                })
            .AddAzureTableGrainStorage(
                "PubSubStore",
                options => options.ConfigureTableServiceClient(GetStorageOptions(context.Configuration).ConnectionString))
            .UseDashboard();

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services) =>
        services
            .Configure<ApplicationOptions>(context.Configuration)
            .Configure<ClusterOptions>(context.Configuration.GetSection(nameof(ApplicationOptions.Cluster)))
            .Configure<StorageOptions>(context.Configuration.GetSection(nameof(ApplicationOptions.Storage)))
            .AddSerializer(serializerBuilder => serializerBuilder.AddJsonSerializer(
                type => type.Namespace is not null && type.Namespace.StartsWith("OrleansTemplate", StringComparison.Ordinal),
                CreateJsonSerializerOptions()));

#if HealthCheck
    private static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
        webHostBuilder
            .UseKestrel(
                (builderContext, options) =>
                {
                    options.AddServerHeader = false;
                    options.Configure(
                        builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)),
                        reloadOnChange: false);
                })
            .UseStartup<Startup>();

#endif
#if Serilog
    /// <summary>
    /// Creates a logger used during application initialisation.
    /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
    /// </summary>
    /// <returns>A logger that can load a new configuration.</returns>
    private static ReloadableLogger CreateBootstrapLogger() =>
        new LoggerConfiguration()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .WriteTo.Debug(formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

    /// <summary>
    /// Configures a logger used during the applications lifetime.
    /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
    /// </summary>
    private static void ConfigureReloadableLogger(
        HostBuilderContext context,
        IServiceProvider services,
        LoggerConfiguration configuration) =>
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
#if ApplicationInsights
            .WriteTo.Conditional(
                x => context.HostingEnvironment.IsProduction(),
                x => x.ApplicationInsights(
                    services.GetRequiredService<TelemetryConfiguration>(),
                    TelemetryConverter.Traces))
#endif
            .WriteTo.Conditional(
                x => context.HostingEnvironment.IsDevelopment(),
                x => x
                    .Console(formatProvider: CultureInfo.InvariantCulture)
                    .WriteTo
                    .Debug(formatProvider: CultureInfo.InvariantCulture));

#endif
    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        var jsonSerializerOptions = new JsonSerializerOptions();
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        jsonSerializerOptions.AddContext<AppJsonSerializerContext>();
        return jsonSerializerOptions;
    }

    private static QueueOptions GetQueueOptions(IConfiguration configuration) =>
        configuration.GetSection(nameof(ApplicationOptions.Queue)).Get<QueueOptions>()!;

    private static StorageOptions GetStorageOptions(IConfiguration configuration) =>
        configuration.GetSection(nameof(ApplicationOptions.Storage)).Get<StorageOptions>()!;
}
