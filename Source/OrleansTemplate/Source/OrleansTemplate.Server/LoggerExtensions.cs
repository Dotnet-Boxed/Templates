namespace OrleansTemplate.Server
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// <see cref="ILogger"/> extension methods. Helps log messages using strongly typing and source generators.
    /// </summary>
#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable CS8795 // Partial method mus have an implementation
    public static partial class LoggerExtensions
    {
        [LoggerMessage(
            EventId = 5000,
            Level = LogLevel.Error,
            Message = "Failed cluster status health check.")]
        public static partial void FailedClusterStatusHealthCheck(this ILogger logger, Exception exception);

        [LoggerMessage(
            EventId = 5001,
            Level = LogLevel.Error,
            Message = "Failed local health check.")]
        public static partial void FailedLocalHealthCheck(this ILogger logger, Exception exception);

        [LoggerMessage(
            EventId = 5002,
            Level = LogLevel.Error,
            Message = "Failed storage health check.")]
        public static partial void FailedStorageHealthCheck(this ILogger logger, Exception exception);
    }
#pragma warning restore CS8795 // Partial method mus have an implementation
#pragma warning restore IDE0060 // Remove unused parameter
}
