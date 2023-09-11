namespace OrleansTemplate.Abstractions.Grains;

public interface IReminderGrain : IGrainWithGuidKey
{
    ValueTask SetReminderAsync(string reminder);
}
