namespace ApiTemplate;

using System.Reflection;
using Boxed.AspNetCore;

internal static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddCustomBootstrapConfiguration(
        this IConfigurationBuilder configurationBuilder, string[] args) =>
        configurationBuilder
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .AddIf(
                args is not null,
                x => x.AddCommandLine(args));

    public static IConfigurationBuilder AddCustomConfiguration(
        this IConfigurationBuilder configurationBuilder,
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
#if ApplicationInsights
            // Push telemetry data through the Azure Application Insights pipeline faster in the development and
            // staging environments, allowing you to view results immediately.
            .AddApplicationInsightsSettings(developerMode: !hostEnvironment.IsProduction())
#endif
            // Add command line options. These take the highest priority.
            .AddIf(
                args is not null,
                x => x.AddCommandLine(args));
}
