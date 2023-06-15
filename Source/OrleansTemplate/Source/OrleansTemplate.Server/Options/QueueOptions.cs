namespace OrleansTemplate.Server.Options;

public class QueueOptions
{
    public string ConnectionString { get; set; } = default!;

    public int CacheSize { get; set; } = default!;

    public List<string> QueueNames { get; set; } = new List<string>();

    public TimeSpan TimerPeriod { get; set; } = default!;
}
