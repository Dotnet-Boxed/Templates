namespace OrleansTemplate.Abstractions.Grains
{
    using System.Threading.Tasks;
    using Orleans;

    /// <summary>
    /// Holds the total count.
    /// </summary>
    /// <remarks>Implemented using the 'Reduce' pattern (See https://github.com/OrleansContrib/DesignPatterns/blob/master/Reduce.md).</remarks>
    /// <seealso cref="IGrainWithGuidKey" />
    public interface ICounterGrain : IGrainWithGuidKey
    {
        Task<long> AddCountAsync(long value);

        Task<long> GetCountAsync();
    }
}
