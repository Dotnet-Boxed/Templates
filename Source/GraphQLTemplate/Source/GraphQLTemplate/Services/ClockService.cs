namespace GraphQLTemplate.Services
{
    using System;

    /// <summary>
    /// Retrieves the current date and/or time. Helps with unit testing by letting you mock the system clock.
    /// </summary>
    public class ClockService : IClockService
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
