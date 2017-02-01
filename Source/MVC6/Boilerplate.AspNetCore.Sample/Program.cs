namespace MvcBoilerplate
{
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    // $Start-PrimaryWebServer-WebListener$
    using Microsoft.Net.Http.Server;
    // $End-PrimaryWebServer-WebListener$

    public sealed class Program
    {
        private const string HostingJsonFileName = "hosting.json";

        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(HostingJsonFileName, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            IHostingEnvironment hostingEnvironment = null;
            var host = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(
                    services =>
                    {
                        hostingEnvironment = services
                            .Where(x => x.ServiceType == typeof(IHostingEnvironment))
                            .Select(x => (IHostingEnvironment)x.ImplementationInstance)
                            .First();
                    })
                // Show error page containing information about startup exceptions when in development.
                .CaptureStartupErrors(hostingEnvironment.IsDevelopment())
                // $Start-PrimaryWebServer-Kestrel$
                .UseKestrel(
                    options =>
                    {
                        // Do not add the Server HTTP header when using the Kestrel Web Server.
                        options.AddServerHeader = false;
                        // $Start-HttpsEverywhere$
                        if (hostingEnvironment.IsDevelopment())
                        {
                            // Use a self-signed certificate to enable 'dotnet run' to work in development.
                            options.UseHttps("DevelopmentCertificate.pfx", "password");
                        }
                        // $End-HttpsEverywhere$
                    })
                // $End-PrimaryWebServer-Kestrel$
                // $Start-PrimaryWebServer-WebListener$
                .UseWebListener(
                    options =>
                    {
                        options.ListenerSettings.Authentication.Schemes = AuthenticationSchemes.None;
                        options.ListenerSettings.Authentication.AllowAnonymous = true;
                    })
                // $End-PrimaryWebServer-WebListener$
                // $Start-CloudProvider$
                .UseAzureAppServices()
                // $End-CloudProvider$
                // $Start-ReverseProxyWebServer-IIS$
                .UseIISIntegration()
                // $End-ReverseProxyWebServer-IIS$
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}