namespace OrleansTemplate.Grains
{
    using System.Threading.Tasks;
    using Orleans;
    using OrleansTemplate.Abstractions.Grains;

    public class HelloGrain : Grain, IHelloGrain
    {
        public Task<string> SayHello(string name) => Task.FromResult($"Hello {name}!");
    }
}
