namespace OrleansTemplate.Abstractions.Grains;

using Orleans;

/// <summary>
/// Holds the total count.
/// </summary>
/// <remarks>Implemented using the 'Reduce' pattern (See https://github.com/OrleansContrib/DesignPatterns/blob/master/Reduce.md).</remarks>
/// <seealso cref="IGrainWithGuidKey" />
public interface ICounterGrain : IGrainWithGuidKey
{
    ValueTask<long> AddCountAsync(long value);

    ValueTask<long> GetCountAsync();
}
