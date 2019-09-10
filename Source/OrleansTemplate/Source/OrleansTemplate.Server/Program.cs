namespace OrleansTemplate.Server
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Statistics;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Grains;
    using OrleansTemplate.Server.Options;
    using Serilog;
    using Serilog.Core;

    public class Program
    {
        public static Task<int> Main(string[] args) => LogAndRun(CreateSiloHostBuilder(args).Build());

        public static async Task<int> LogAndRun(ISiloHost siloHost)
        {
            Log.Logger = BuildLogger(siloHost.Services.GetRequiredService<IConfiguration>());

            try
            {
                Log.Information("Starting application");
                await siloHost.StartAsync();
                Log.Information("Started application, press CTRL+C to exit");

                var siloStopped = new ManualResetEvent(false);
                void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
                {
                    Console.CancelKeyPress -= OnCancelKeyPress; // Remove handler to stop listening to CTRL+C events.
                    e.Cancel = true;                            // Prevent the application from crashing ungracefully.
                    Task.Run(async () =>                        // Shutdown gracefully on a background thread.
                    {
                        try
                        {
                            Log.Information("Stopping application");
                            await siloHost.StopAsync();
                            Log.Information("Stopped application");

                            siloStopped.Set();
                        }
                        catch (Exception exception)
                        {
                            Log.Fatal(exception, "Application stopped ungracefully");
                        }
                    });
                }
                Console.CancelKeyPress += OnCancelKeyPress;

                siloStopped.WaitOne();

                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Application terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static ISiloHostBuilder CreateSiloHostBuilder(string[] args)
        {
            StorageOptions storageOptions = null;
            return new SiloHostBuilder()
                // Prevent the silo from automatically stopping itself when the cancel key is pressed.
                .Configure<ProcessExitHandlingOptions>(options => options.FastKillOnProcessExit = false)
                .ConfigureAppConfiguration(
                    (context, configurationBuilder) =>
                    {
                        context.HostingEnvironment.EnvironmentName = GetEnvironmentName();
                        AddConfiguration(configurationBuilder, context.HostingEnvironment.EnvironmentName, args);
                    })
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

                        storageOptions = services.BuildServiceProvider().GetRequiredService<IOptions<StorageOptions>>().Value;
                    })
                .UseAzureStorageClustering(options => options.ConnectionString = storageOptions.ConnectionString)
                .ConfigureEndpoints(
                    EndpointOptions.DEFAULT_SILO_PORT,
                    EndpointOptions.DEFAULT_GATEWAY_PORT,
                    listenOnAnyHostAddress: !IsRunningInDevelopment())
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
#if ApplicationInsights
                .AddApplicationInsightsTelemetryConsumer()
#endif
                .ConfigureLogging(logging => logging.AddSerilog())
                .AddAzureTableGrainStorageAsDefault(
                    options =>
                    {
                        options.ConnectionString = storageOptions.ConnectionString;
                        options.ConfigureJsonSerializerSettings = ConfigureJsonSerializerSettings;
                        options.UseJson = true;
                    })
                .UseAzureTableReminderService(options => options.ConnectionString = storageOptions.ConnectionString)
                .UseTransactions(withStatisticsReporter: true)
                .AddAzureTableTransactionalStateStorageAsDefault(options => options.ConnectionString = storageOptions.ConnectionString)
                .AddSimpleMessageStreamProvider(StreamProviderName.Default)
                .AddAzureTableGrainStorage(
                    "PubSubStore",
                    options =>
                    {
                        options.ConnectionString = storageOptions.ConnectionString;
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
        }

        private static IConfigurationBuilder AddConfiguration(
            IConfigurationBuilder configurationBuilder,
            string environmentName,
            string[] args) =>
            configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                // Add configuration from the appsettings.json file.
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                // Add configuration from an optional appsettings.development.json, appsettings.staging.json or
                // appsettings.production.json file, depending on the environment. These settings override the ones in
                // the appsettings.json file.
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    string.Equals(environmentName, EnvironmentName.Development, StringComparison.Ordinal),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true))
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
                // Add command line options. These take the highest priority.
                .AddIf(
                    args != null,
                    x => x.AddCommandLine(args));

        private static Logger BuildLogger(IConfiguration configuration) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Application", GetAssemblyProductName())
                .Enrich.With(new TraceIdEnricher())
                .CreateLogger();

        private static void ConfigureJsonSerializerSettings(JsonSerializerSettings jsonSerializerSettings)
        {
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
        }

        private static bool IsRunningInDevelopment() =>
            string.Equals(GetEnvironmentName(), EnvironmentName.Development, StringComparison.Ordinal);

        private static string GetEnvironmentName() =>
            Environment.GetEnvironmentVariable("ENVIRONMENT") ?? EnvironmentName.Production;

        private static string GetAssemblyProductName() =>
            Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;
    }
}
