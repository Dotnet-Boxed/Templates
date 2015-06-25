namespace MvcBoilerplate.Services
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public sealed class LoggingService : ILoggingService
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Log(Exception exception)
        {
            // Log to Tracing.
            Trace.TraceError(exception.ToString());
            // TODO: Elmah does not exist for ASP.NET 5
            // Log to Elmah.
            // ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
        }
    }
}