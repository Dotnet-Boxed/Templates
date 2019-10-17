namespace ApiTemplate
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using ApiTemplate.Options;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Core;

    public sealed class Program
    {
        public static Task<int> Main(string[] args) => LogAndRunAsync(CreateHostBuilder(args).Build());

        public static async Task<int> LogAndRunAsync(IHost host)
        {
            Log.Logger = CreateLogger(host);

            try
            {
                Log.Information("Starting application");
                await host.RunAsync().ConfigureAwait(false);
                Log.Information("Stopped application");
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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureHostConfiguration(
                    configurationBuilder => configurationBuilder
                        .AddEnvironmentVariables(prefix: "DOTNET_")
                        .AddIf(
                            args != null,
                            x => x.AddCommandLine(args)))
                .ConfigureAppConfiguration((hostingContext, config) =>
                    AddConfiguration(config, hostingContext.HostingEnvironment, args))
                .UseSerilog()
                .UseDefaultServiceProvider(
                    (context, options) =>
                    {
                        var isDevelopment = context.HostingEnvironment.IsDevelopment();
                        options.ValidateScopes = isDevelopment;
                        options.ValidateOnBuild = isDevelopment;
                    })
                .ConfigureWebHost(ConfigureWebHostBuilder);

        private static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
            webHostBuilder
                .UseKestrel(
                    (builderContext, options) =>
                    {
                        // Do not add the Server HTTP header.
                        options.AddServerHeader = false;

                        // Configure Kestrel from appsettings.json.
                        options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)));
                        ConfigureKestrelServerLimits(builderContext.Configuration, options);
                    })
#if Azure
                .UseAzureAppServices()
#endif
                // Used for IIS and IIS Express for in-process hosting. Use UseIISIntegration for out-of-process hosting.
                .UseIIS()
                .UseStartup<Startup>();

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
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true))
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
#if ApplicationInsights
                // Push telemetry data through the Azure Application Insights pipeline faster in the development and
                // staging environments, allowing you to view results immediately.
                .AddApplicationInsightsSettings(developerMode: !hostEnvironment.IsProduction())
#endif
                // Add command line options. These take the highest priority.
                .AddIf(
                    args != null,
                    x => x.AddCommandLine(args));

        private static Logger CreateLogger(IHost host) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(host.Services.GetRequiredService<IConfiguration>())
                .Enrich.WithProperty("Application", GetAssemblyProductName())
                .CreateLogger();

        private static string GetAssemblyProductName() =>
            Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;

        /// <summary>
        /// Configure Kestrel server limits from appsettings.json is not supported. So we manually copy them from config.
        /// See https://github.com/aspnet/KestrelHttpServer/issues/2216
        /// </summary>
        private static void ConfigureKestrelServerLimits(
            IConfiguration configuration,
            KestrelServerOptions options)
        {
            var source = configuration
                .GetSection(nameof(ApplicationOptions.Kestrel))
                .Get<KestrelServerOptions>();

            var limits = options.Limits;
            var sourceLimits = source.Limits;

            var http2 = limits.Http2;
            var sourceHttp2 = sourceLimits.Http2;

            http2.HeaderTableSize = sourceHttp2.HeaderTableSize;
            http2.InitialConnectionWindowSize = sourceHttp2.InitialConnectionWindowSize;
            http2.InitialStreamWindowSize = sourceHttp2.InitialStreamWindowSize;
            http2.MaxFrameSize = sourceHttp2.MaxFrameSize;
            http2.MaxRequestHeaderFieldSize = sourceHttp2.MaxRequestHeaderFieldSize;
            http2.MaxStreamsPerConnection = sourceHttp2.MaxStreamsPerConnection;

            limits.KeepAliveTimeout = sourceLimits.KeepAliveTimeout;
            limits.MaxConcurrentConnections = sourceLimits.MaxConcurrentConnections;
            limits.MaxConcurrentUpgradedConnections = sourceLimits.MaxConcurrentUpgradedConnections;
            limits.MaxRequestBodySize = sourceLimits.MaxRequestBodySize;
            limits.MaxRequestBufferSize = sourceLimits.MaxRequestBufferSize;
            limits.MaxRequestHeaderCount = sourceLimits.MaxRequestHeaderCount;
            limits.MaxRequestHeadersTotalSize = sourceLimits.MaxRequestHeadersTotalSize;
            limits.MaxRequestLineSize = sourceLimits.MaxRequestLineSize;
            limits.MaxResponseBufferSize = sourceLimits.MaxResponseBufferSize;
            limits.MinRequestBodyDataRate = sourceLimits.MinRequestBodyDataRate;
            limits.MinResponseDataRate = sourceLimits.MinResponseDataRate;
            limits.RequestHeadersTimeout = sourceLimits.RequestHeadersTimeout;
        }
    }
}
