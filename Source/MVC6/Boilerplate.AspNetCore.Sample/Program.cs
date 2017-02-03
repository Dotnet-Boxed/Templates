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

            // $Start-HttpsEverywhere$
            IHostingEnvironment hostingEnvironment = null;
            // $End-HttpsEverywhere$
            var host = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
                // $Start-HttpsEverywhere$
                .ConfigureServices(
                    services =>
                    {
                        hostingEnvironment = services
                            .Where(x => x.ServiceType == typeof(IHostingEnvironment))
                            .Select(x => (IHostingEnvironment)x.ImplementationInstance)
                            .First();
                    })
                // $End-HttpsEverywhere$
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
                            // This will give a browser security warning which you can safely ignore.
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