namespace OrleansTemplate.Abstractions.Grains;

public interface IHelloGrain : IGrainWithGuidKey
{
    ValueTask<string> SayHelloAsync(string name);
}
