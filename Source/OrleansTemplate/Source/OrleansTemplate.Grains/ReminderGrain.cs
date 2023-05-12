namespace OrleansTemplate.Grains;

using System.Threading;
using Orleans.Runtime;
using OrleansTemplate.Abstractions.Constants;
using OrleansTemplate.Abstractions.Grains;

public class ReminderGrain : Grain, IReminderGrain, IRemindable
{
    private const string ReminderName = "SomeReminder";
    private string? reminder;

    public ValueTask SetReminderAsync(string reminder)
    {
        this.reminder = reminder;
        return ValueTask.CompletedTask;
    }

    public override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // Reminders are timers that are persisted to storage, so they are resilient if the node goes down. They
        // should not be used for high-frequency timers their period should be measured in minutes, hours or days.
        await this.RegisterOrUpdateReminder(
                ReminderName,
                TimeSpan.FromSeconds(150),
                TimeSpan.FromSeconds(60))
            .ConfigureAwait(true);
        await base.OnActivateAsync(cancellationToken).ConfigureAwait(true);
    }

    public async Task ReceiveReminder(string reminderName, TickStatus status)
    {
        if (string.Equals(ReminderName, reminderName, StringComparison.Ordinal))
        {
            if (!string.IsNullOrEmpty(this.reminder))
            {
                await this.PublishReminderAsync(this.reminder).ConfigureAwait(true);
            }
        }
    }

    private Task PublishReminderAsync(string reminder)
    {
        var streamProvider = this.GetStreamProvider(StreamProviderName.Default);
        var stream = streamProvider.GetStream<string>(StreamId.Create(StreamName.Reminder, Guid.Empty));
        return stream.OnNextAsync(reminder);
    }
}
