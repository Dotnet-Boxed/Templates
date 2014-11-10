namespace MvcBoilerplate.Services
{
    using System;
    using System.Diagnostics;
    using Elmah;

    public sealed class LoggingService : ILoggingService
    {
        public void Log(Exception exception)
        {
            Trace.TraceError(exception.ToString());
            ErrorSignal.FromCurrentContext().Raise(exception);
        }
    }
}