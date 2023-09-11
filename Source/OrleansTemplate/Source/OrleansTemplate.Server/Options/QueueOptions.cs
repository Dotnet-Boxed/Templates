namespace OrleansTemplate.Server.Options;

public class QueueOptions
{
    public string ConnectionString { get; set; } = default!;

    public int CacheSize { get; set; } = default!;

    public string QueueNames { get; set; } = default!;

    public List<string> QueueNamesCollection => new(this.QueueNames.Split(','));

    public TimeSpan TimerPeriod { get; set; } = default!;
}
