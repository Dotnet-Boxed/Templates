namespace OrleansTemplate.Client
{
    using System;
    using System.Threading.Tasks;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using OrleansTemplate.Abstractions.Constants;
    using OrleansTemplate.Abstractions.Grains;

    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                var clusterClient = CreateClientBuilder().Build();
                await clusterClient.Connect();
                RequestContext.Set("TraceId", Guid.NewGuid());

                Console.WriteLine("What is your name?");
                var name = Console.ReadLine();
                var helloGrain = clusterClient.GetGrain<IHelloGrain>(Guid.NewGuid());
                Console.WriteLine(await helloGrain.SayHello(name));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return -1;
            }

            return 0;
        }

        private static IClientBuilder CreateClientBuilder() =>
            new ClientBuilder()
                .UseAzureStorageClustering(options => options.ConnectionString = "UseDevelopmentStorage=true")
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = Cluster.ClusterId;
                    options.ServiceId = Cluster.ServiceId;
                })
                .ConfigureApplicationParts(
                    parts => parts
                        .AddApplicationPart(typeof(ICounterGrain).Assembly)
                        .WithReferences())
                .AddSimpleMessageStreamProvider(StreamProviderName.Default);
    }
}
