namespace GraphQLTemplate.Services;

/// <summary>
/// Retrieves the current date and/or time. Helps with unit testing by letting you mock the system clock.
/// </summary>
public interface IClockService
{
    DateTimeOffset UtcNow { get; }
}
