namespace MvcBoilerplate
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Diagnostics;
    using Microsoft.AspNet.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public partial class Startup
    {
        private const string LoggingConfigurationSectionName = "Logging";

        /// <summary>
        /// Configure tools used to help with logging internal application events.
        /// See http://docs.asp.net/en/latest/fundamentals/logging.html
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development, 
        /// Staging or Production by default.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are 
        /// stored.</param>
        private static void ConfigureLogging(
            IApplicationBuilder application,
            IHostingEnvironment environment,
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            // Add the following to the request pipeline only in development environment.
            if (environment.IsDevelopment())
            {
                // Set the minimum logging level to log all log messages. See details below:
                loggerFactory.MinimumLevel = LogLevel.Debug;
                // Debug - Used for the most detailed log messages, typically only valuable to a developer debugging an 
                //     issue.These messages may contain sensitive application data and so should not be enabled in a 
                //     production environment.Disabled by default.
                //     Example: 'Credentials: { "User":"someuser", "Password":"P@ssword"}'.
                // Verbose - These messages have short-term usefulness during development.They contain information that 
                //     may be useful for debugging, but have no long - term value.This is the default most verbose 
                //     level of logging. Example: 'Entering method Configure with flag set to true'.
                // Information - These messages are used to track the general flow of the application.These logs should 
                //     have some long term value, as opposed to Verbose level messages, which do not.
                //     Example: 'Request received for path / foo'.
                // Warning - The Warning level should be used for abnormal or unexpected events in the application 
                //     flow.These may include errors or other conditions that do not cause the application to stop, but 
                //     which may need to be investigated in the future. Handled exceptions are a common place to use 
                //     the Warning log level. 
                //     Examples: 'Login failed for IP 127.0.0.1' or 'FileNotFoundException for file foo.txt'.
                // Error - An error should be logged when the current flow of the application must stop due to some 
                //     failure, such as an exception that cannot be handled or recovered from.These messages should 
                //     indicate a failure in the current activity or operation(such as the current HTTP request), not 
                //     an application - wide failure. Example: 'Cannot insert record due to duplicate key violation'.
                // Critical - A critical log level should be reserved for unrecoverable application or system crashes, 
                //     or catastrophic failure that requires immediate attention.
                //     Examples: 'data loss scenarios', 'stack overflows', 'out of disk space'.

                // Add the console logger.
                loggerFactory.AddConsole(configuration.GetSection(LoggingConfigurationSectionName));

                // Add the debug logger.
                loggerFactory.AddDebug();
            }
            else
            {
                // Set the minimum logging level to log information level log messages. See details above:
                loggerFactory.MinimumLevel = LogLevel.Information;

                // Log warning level messages and above to the Windows event log.
                // var sourceSwitch = new SourceSwitch("EventLog");
                // sourceSwitch.Level = SourceLevels.Warning;
                // loggerFactory.AddTraceSource(sourceSwitch, new EventLogTraceListener("Application"));

                // Log to NLog (See https://github.com/aspnet/Logging/tree/1.0.0-beta6/src/Microsoft.Framework.Logging.NLog).
                // loggerFactory.AddNLog(new NLog.LogFactory());

                // Log to Serilog (See https://github.com/serilog/serilog-framework-logging).
                // loggerFactory.AddSerilog();
            }
        }
    }
}