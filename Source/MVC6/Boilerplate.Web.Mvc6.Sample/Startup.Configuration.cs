namespace MvcBoilerplate
{
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.Configuration;
    // $Start-ApplicationInsights$
    using Microsoft.Extensions.DependencyInjection;
    // $End-ApplicationInsights$
    using Microsoft.Extensions.PlatformAbstractions;

    public partial class Startup
    {
        /// <summary>
        /// Creates and configures the application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// http://weblog.west-wind.com/posts/2015/Jun/03/Strongly-typed-AppSettings-Configuration-in-ASPNET-5
        /// </summary>
        /// <param name="applicationEnvironment">The location the application is running in</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <returns>A collection of key value pair settings.</returns>
        private static IConfiguration ConfigureConfiguration(
            IApplicationEnvironment applicationEnvironment,
            IHostingEnvironment hostingEnvironment)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath);

            // Add configuration from the config.json file.
            configurationBuilder.AddJsonFile("config.json");

            // Add configuration from an optional config.development.json, config.staging.json or 
            // config.production.json file, depending on the environment. These settings override the ones in the 
            // config.json file.
            configurationBuilder.AddJsonFile($"config.{hostingEnvironment.EnvironmentName}.json", optional: true);

            // This reads the configuration keys from the secret store. This allows you to store connection strings
            // and other sensitive settings, so you don't have to check them into your source control provider. See 
            // http://go.microsoft.com/fwlink/?LinkID=532709 and
            // http://docs.asp.net/en/latest/security/app-secrets.html
            configurationBuilder.AddUserSecrets();

            // Add configuration specific to the Development, Staging or Production environments. This config can 
            // be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings 
            // override the ones in all of the above config files.
            // Note: To set environment variables for debugging navigate to:
            // Project Properties -> Debug Tab -> Environment Variables
            // Note: To get environment variables for the machine use the following command in PowerShell:
            // $env:[VARIABLE_NAME]
            // Note: To set environment variables for the machine use the following command in PowerShell:
            // $env:[VARIABLE_NAME]="[VARIABLE_VALUE]"
            // Note: Environment variables use a colon separator e.g. You can override the site title by creating a 
            // variable named AppSettings:SiteTitle. See 
            // http://docs.asp.net/en/latest/security/app-secrets.html
            configurationBuilder.AddEnvironmentVariables();

            // $Start-ApplicationInsights$
            // Push telemetry data through the Azure Application Insights pipeline faster in the development and 
            // staging environments, allowing you to view results immediately.
            configurationBuilder.AddApplicationInsightsSettings(developerMode: !hostingEnvironment.IsProduction());

            // $End-ApplicationInsights$
            return configurationBuilder.Build();
        }
    }
}
