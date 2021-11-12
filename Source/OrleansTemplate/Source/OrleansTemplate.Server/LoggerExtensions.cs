namespace OrleansTemplate.Server;

/// <summary>
/// <see cref="ILogger"/> extension methods. Helps log messages using strongly typing and source generators.
/// </summary>
internal static partial class LoggerExtensions
{
    [LoggerMessage(
        EventId = 5000,
        Level = LogLevel.Information,
        Message = "Started {Application} in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationStarted(
        this ILogger logger,
        string application,
        string environment,
        string runtime,
        string operatingSystem);

    [LoggerMessage(
        EventId = 5001,
        Level = LogLevel.Information,
        Message = "Stopped {Application} in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationStopped(
        this ILogger logger,
        string application,
        string environment,
        string runtime,
        string operatingSystem);

    [LoggerMessage(
        EventId = 5002,
        Level = LogLevel.Critical,
        Message = "{Application} terminated unexpectedly in {Environment} mode with runtime {Runtime} and OS {OperatingSystem}.")]
    public static partial void ApplicationTerminatedUnexpectedly(
        this ILogger logger,
        Exception exception,
        string application,
        string environment,
        string runtime,
        string operatingSystem);

    [LoggerMessage(
        EventId = 5003,
        Level = LogLevel.Error,
        Message = "Failed cluster status health check.")]
    public static partial void FailedClusterStatusHealthCheck(this ILogger logger, Exception exception);

    [LoggerMessage(
        EventId = 5004,
        Level = LogLevel.Error,
        Message = "Failed local health check.")]
    public static partial void FailedLocalHealthCheck(this ILogger logger, Exception exception);

    [LoggerMessage(
        EventId = 5005,
        Level = LogLevel.Error,
        Message = "Failed storage health check.")]
    public static partial void FailedStorageHealthCheck(this ILogger logger, Exception exception);
}
