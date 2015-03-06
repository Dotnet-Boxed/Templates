namespace MvcBoilerplate.Services
{
    using System;
    using System.Diagnostics;
    using Elmah;

    public sealed class LoggingService : ILoggingService
    {
        public void Log(Exception exception)
        {
            // Log to Tracing.
            Trace.TraceError(exception.ToString());
            // Log to Elmah.
            ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}