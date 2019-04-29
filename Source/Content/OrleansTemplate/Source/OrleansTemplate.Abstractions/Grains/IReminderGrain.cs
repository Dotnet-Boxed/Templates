namespace OrleansTemplate.Abstractions.Grains
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IReminderGrain : IGrainWithGuidKey
    {
        Task SetReminder(string reminder);
    }
}
