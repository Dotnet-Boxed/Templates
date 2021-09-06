namespace OrleansTemplate.Abstractions.Grains
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IHelloGrain : IGrainWithGuidKey
    {
        ValueTask<string> SayHelloAsync(string name);
    }
}
