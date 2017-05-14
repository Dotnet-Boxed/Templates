namespace ApiTemplate.Services
{
    using System;

    /// <summary>
    /// Retrieves the current time. Helps with unit testing by letting you mock the system clock.
    /// </summary>
    public interface IClockService
    {
        DateTimeOffset UtcNow { get; }
    }
}
