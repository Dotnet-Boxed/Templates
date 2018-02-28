namespace ApiTemplate
{
    using System;
    using System.IO;
    using System.Reflection;
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Core;

    public sealed class Program
    {
        public static int Main(string[] args) => LogAndRun(BuildWebHost(args));

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

        public static IWebHost BuildWebHost(string[] args) =>
            new WebHostBuilder()
                .UseKestrel(
                    options =>
                    {
                        options.AddServerHeader = false; // Do not add the Server HTTP header.
                        options.ConfigureEndpoints();
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                    AddConfiguration(config, hostingContext.HostingEnvironment, args))
#if (Azure)
                .UseAzureAppServices()
#endif
#if (IIS && !Azure)
                .UseIISIntegration()
#endif
                .UseDefaultServiceProvider((context, options) =>
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseSerilog()
                .UseStartup<Startup>()
                .Build();

        private static IConfigurationBuilder AddConfiguration(
            IConfigurationBuilder configurationBuilder,
            IHostingEnvironment hostingEnvironment,
            string[] args) =>
            configurationBuilder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true))
                .AddEnvironmentVariables()
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
    }
}