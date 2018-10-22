namespace ApiTemplate
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using ApiTemplate.Options;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Core;

    public sealed class Program
    {
        public static int Main(string[] args) => LogAndRun(CreateWebHostBuilder(args).Build());

        public static int LogAndRun(IWebHost webHost)
        {
            Log.Logger = BuildLogger(webHost);

            try
            {
                Log.Information("Starting application");
                webHost.Run();
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                .UseKestrel(
                    (builderContext, options) =>
                    {
                        // Do not add the Server HTTP header.
                        options.AddServerHeader = false;

                        // Configure Kestrel from appsettings.json.
                        options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)));
                        ConfigureKestrelServerLimits(builderContext, options);
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                    AddConfiguration(config, hostingContext.HostingEnvironment, args))
                .UseSerilog()
#if (Azure)
                .UseAzureAppServices()
#endif
#if (IIS && !Azure)
                .UseIISIntegration()
#endif
                .UseDefaultServiceProvider((context, options) =>
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseStartup<Startup>();

        private static IConfigurationBuilder AddConfiguration(
            IConfigurationBuilder configurationBuilder,
            IHostingEnvironment hostingEnvironment,
            string[] args) =>
            configurationBuilder
                // Add configuration from the appsettings.json file.
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                // Add configuration from an optional appsettings.development.json, appsettings.staging.json or
                // appsettings.production.json file, depending on the environment. These settings override the ones in
                // the appsettings.json file.
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                // This reads the configuration keys from the secret store. This allows you to store connection strings
                // and other sensitive settings, so you don't have to check them into your source control provider.
                // Only use this in Development, it is not intended for Production use. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true))
                // Add configuration specific to the Development, Staging or Production environments. This config can
                // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
                // override the ones in all of the above config files. See
                // http://docs.asp.net/en/latest/security/app-secrets.html
                .AddEnvironmentVariables()
#if (ApplicationInsights)
                // Push telemetry data through the Azure Application Insights pipeline faster in the development and
                // staging environments, allowing you to view results immediately.
                .AddApplicationInsightsSettings(developerMode: !hostingEnvironment.IsProduction())
#endif
                // Add command line options. These take the highest priority.
                .AddIf(
                    args != null,
                    x => x.AddCommandLine(args));

        private static Logger BuildLogger(IWebHost webHost) =>
            new LoggerConfiguration()
                .ReadFrom.Configuration(webHost.Services.GetRequiredService<IConfiguration>())
                .Enrich.WithProperty("Application", GetAssemblyProductName())
                .CreateLogger();

        private static string GetAssemblyProductName() =>
            Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;

        /// <summary>
        /// Configure Kestrel server limits from appsettings.json is not supported. So we manually copy them from config.
        /// See https://github.com/aspnet/KestrelHttpServer/issues/2216
        /// </summary>
        private static void ConfigureKestrelServerLimits(
            WebHostBuilderContext builderContext,
            KestrelServerOptions options)
        {
            var kestrelOptions = new KestrelServerOptions();
            builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)).Bind(kestrelOptions);
            foreach (var property in typeof(KestrelServerLimits).GetProperties())
            {
                if (property.PropertyType == typeof(MinDataRate))
                {
                    var section = builderContext.Configuration.GetSection(
                        $"{nameof(ApplicationOptions.Kestrel)}:{nameof(KestrelServerOptions.Limits)}");
                    if (section.GetChildren().Any(x => string.Equals(x.Key, property.Name, StringComparison.Ordinal)))
                    {
                        var bytesPerSecond = section.GetValue<double>($"{property.Name}:{nameof(MinDataRate.BytesPerSecond)}");
                        var gracePeriod = section.GetValue<TimeSpan>($"{property.Name}:{nameof(MinDataRate.GracePeriod)}");
                        property.SetValue(options.Limits, new MinDataRate(bytesPerSecond, gracePeriod));
                    }
                }
                else
                {
                    var value = property.GetValue(kestrelOptions.Limits);
                    property.SetValue(options.Limits, value);
                }
            }
        }
    }
}