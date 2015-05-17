namespace MvcBoilerplate.Services
{
    using System;
    
    public interface ILoggingService
    {
        void Log(Exception exception);
    }
}
