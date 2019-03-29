namespace OrleansTemplate.Abstractions.Grains
{
    using System.Threading.Tasks;
    using Orleans;

    public interface IHelloGrain : IGrainWithGuidKey
    {
        Task<string> SayHello(string name);
    }
}
