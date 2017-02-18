namespace ApiTemplate
{
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
#if (WebListener)
    using Microsoft.Net.Http.Server;
#endif

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

#if (HttpsEverywhere)
            IHostingEnvironment hostingEnvironment = null;
#endif
            var host = new WebHostBuilder()
                .UseConfiguration(configuration)
                .UseContentRoot(Directory.GetCurrentDirectory())
#if (HttpsEverywhere)
                .ConfigureServices(
                    services =>
                    {
                        hostingEnvironment = services
                            .Where(x => x.ServiceType == typeof(IHostingEnvironment))
                            .Select(x => (IHostingEnvironment)x.ImplementationInstance)
                            .First();
                    })
#endif
#if (Kestrel)
                .UseKestrel(
                    options =>
                    {
                        // Do not add the Server HTTP header when using the Kestrel Web Server.
                        options.AddServerHeader = false;
#if (HttpsEverywhere)

                        if (hostingEnvironment.IsDevelopment())
                        {
                            // Use a self-signed certificate to enable 'dotnet run' to work in development.
                            // This will give a browser security warning which you can safely ignore.
                            options.UseHttps("DevelopmentCertificate.pfx", "password");
                        }
#endif
                    })
#endif
#if (WebListener)
                .UseWebListener(
                    options =>
                    {
                        options.ListenerSettings.Authentication.Schemes = AuthenticationSchemes.None;
                        options.ListenerSettings.Authentication.AllowAnonymous = true;
                    })
#endif
#if (Azure)
                .UseAzureAppServices()
#endif
#if (IIS && !Azure)
                .UseIISIntegration()
#endif
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}