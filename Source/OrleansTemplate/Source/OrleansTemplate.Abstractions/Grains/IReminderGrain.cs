namespace OrleansTemplate.Abstractions.Grains;

using System.Threading.Tasks;
using Orleans;

public interface IReminderGrain : IGrainWithGuidKey
{
    ValueTask SetReminderAsync(string reminder);
}
