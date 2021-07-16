namespace OrleansTemplate.Server
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
#if ApplicationInsights
    using Microsoft.ApplicationInsights.Extensibility;
#endif
#if HealthCheck
    using Microsoft.AspNetCore.Hosting;
#endif
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using Orleans.Statistics;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Grains;
    using OrleansTemplate.Server.Options;
#if Serilog
    using Serilog;
    using Serilog.Extensions.Hosting;
#endif

    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
#if Serilog
            Log.Logger = CreateBootstrapLogger();
#endif
            IHostEnvironment? hostEnvironment = null;

            try
            {
#if Serilog
                Log.Information("Initialising.");
#endif
                var host = CreateHostBuilder(args).Build();
                hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
                hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;

#if Serilog
                Log.Information(
                    "Started {Application} in {Environment} mode.",
                    hostEnvironment.ApplicationName,
                    hostEnvironment.EnvironmentName);
#endif
                await host.RunAsync().ConfigureAwait(false);
#if Serilog
                Log.Information(
                    "Stopped {Application} in {Environment} mode.",
                    hostEnvironment.ApplicationName,
                    hostEnvironment.EnvironmentName);
#endif
                return 0;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
            {
#if Serilog
                Log.Fatal(
                    exception,
                    "{Application} terminated unexpectedly in {Environment} mode.",
                    AssemblyInformation.Current.Product,
                    hostEnvironment?.EnvironmentName);
#else
                Console.WriteLine($"{AssemblyInformation.Current.Product} terminated unexpectedly in {hostEnvironment?.EnvironmentName} mode.");
                Console.WriteLine(exception.ToString());
#endif

                return 1;
            }
#if Serilog
            finally
            {
                Log.CloseAndFlush();
            }
#endif
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(
                    configurationBuilder => configurationBuilder
                        .AddEnvironmentVariables(prefix: "DOTNET_")
                        .AddIf(
                            args is not null,
                            x => x.AddCommandLine(args)))
                .ConfigureAppConfiguration((hostingContext, config) =>
                    AddConfiguration(config, hostingContext.HostingEnvironment, args))
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
            Microsoft.Extensions.Hosting.HostBuilderContext context,
            ISiloBuilder siloBuilder) =>
            siloBuilder
                .ConfigureServices(
                    (context, services) =>
                    {
                        services.Configure<ApplicationOptions>(context.Configuration);
                        services.Configure<ClusterOptions>(context.Configuration.GetSection(nameof(ApplicationOptions.Cluster)));
                        services.Configure<StorageOptions>(context.Configuration.GetSection(nameof(ApplicationOptions.Storage)));
#if ApplicationInsights
                        services.Configure<ApplicationInsightsTelemetryConsumerOptions>(
                            context.Configuration.GetSection(nameof(ApplicationOptions.ApplicationInsights)));
#endif
                    })
                .UseSiloUnobservedExceptionsHandler()
                .UseAzureStorageClustering(
                    options => options.ConnectionString = GetStorageOptions(context.Configuration).ConnectionString)
                .ConfigureEndpoints(
                    EndpointOptions.DEFAULT_SILO_PORT,
                    EndpointOptions.DEFAULT_GATEWAY_PORT,
                    listenOnAnyHostAddress: !context.HostingEnvironment.IsDevelopment())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
#if ApplicationInsights
                .AddApplicationInsightsTelemetryConsumer()
#endif
                .AddAzureTableGrainStorageAsDefault(
                    options =>
                    {
                        options.ConnectionString = GetStorageOptions(context.Configuration).ConnectionString;
                        options.ConfigureJsonSerializerSettings = ConfigureJsonSerializerSettings;
                        options.UseJson = true;
                    })
                .UseAzureTableReminderService(
                    options => options.ConnectionString = GetStorageOptions(context.Configuration).ConnectionString)
                .UseTransactions(withStatisticsReporter: true)
                .AddAzureTableTransactionalStateStorageAsDefault(
                    options => options.ConnectionString = GetStorageOptions(context.Configuration).ConnectionString)
                .AddSimpleMessageStreamProvider(StreamProviderName.Default)
                .AddAzureTableGrainStorage(
                    "PubSubStore",
                    options =>
                    {
                        options.ConnectionString = GetStorageOptions(context.Configuration).ConnectionString;
                        options.ConfigureJsonSerializerSettings = ConfigureJsonSerializerSettings;
                        options.UseJson = true;
                    })
                .UseIf(
                    RuntimeInformation.IsOSPlatform(OSPlatform.Linux),
                    x => x.UseLinuxEnvironmentStatistics())
                .UseIf(
                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
                    x => x.UsePerfCounterEnvironmentStatistics())
                .UseDashboard();

#if HealthCheck
        private static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
            webHostBuilder
                .UseKestrel(
                    (builderContext, options) =>
                    {
                        options.AddServerHeader = false;
                        options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)), reloadOnChange: false);
                    })
                .UseStartup<Startup>();

#endif
        private static IConfigurationBuilder AddConfiguration(
            IConfigurationBuilder configurationBuilder,
            IHostEnvironment hostEnvironment,
            string[] args) =>
            configurationBuilder
                // Add configuration from the appsettings.json file.
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                // Add configuration from an optional appsettings.development.json, appsettings.staging.json or
                // appsettings.production.json file, depending on the environment. These settings override the ones in
                // the appsettings.json file.
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: false)
                // Add configuration from files in the specified directory. The name of the file is the key and the
                // contents the value.
                .AddKeyPerFile(Path.Combine(Directory.GetCurrentDirectory(), "configuration"), optional: true, reloadOnChange: false)
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: false))
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
                // Add command line options. These take the highest priority.
                .AddIf(
                    args is not null,
                    x => x.AddCommandLine(args));
#if Serilog

        /// <summary>
        /// Creates a logger used during application initialisation.
        /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
        /// </summary>
        /// <returns>A logger that can load a new configuration.</returns>
        private static ReloadableLogger CreateBootstrapLogger() =>
            new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateBootstrapLogger();

        /// <summary>
        /// Configures a logger used during the applications lifetime.
        /// <see href="https://nblumhardt.com/2020/10/bootstrap-logger/"/>.
        /// </summary>
        private static void ConfigureReloadableLogger(
            Microsoft.Extensions.Hosting.HostBuilderContext context,
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
                    x => x.Console().WriteTo.Debug());
#endif

        private static void ConfigureJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
        }

        private static StorageOptions GetStorageOptions(IConfiguration configuration) =>
            configuration.GetSection(nameof(ApplicationOptions.Storage)).Get<StorageOptions>();
    }
}
