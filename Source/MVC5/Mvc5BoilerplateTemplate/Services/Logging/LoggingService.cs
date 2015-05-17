namespace $safeprojectname$.Services
{
    using System;
    using System.Diagnostics;
    using System.Web;
    using Elmah;

    public sealed class LoggingService : ILoggingService
    {
        public void Log(Exception exception)
        {
            // Log to Tracing.
            Trace.TraceError(exception.ToString());
            // Log to Elmah.
            ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
        }
    }
}