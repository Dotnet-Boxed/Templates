namespace $safeprojectname$.Services
{
    using System;
    
    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Log(Exception exception);
    }
}
