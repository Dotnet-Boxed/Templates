namespace OrleansTemplate.Client
{
    using System;
    using System.Threading.Tasks;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;
    using Orleans.Runtime;
    using Orleans.Streams;
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

                // Set a trace ID, so that requests can be identified.
                RequestContext.Set("TraceId", Guid.NewGuid());

                var reminderGrain = clusterClient.GetGrain<IReminderGrain>(Guid.Empty);
                await reminderGrain.SetReminder("Don't forget to say hello!");

                var streamProvider = clusterClient.GetStreamProvider(StreamProviderName.Default);
                var saidHelloStream = streamProvider.GetStream<string>(Guid.Empty, StreamName.SaidHello);
                var saidHelloSubscription = await saidHelloStream.SubscribeAsync(OnSaidHello);
                var reminderStream = streamProvider.GetStream<string>(Guid.Empty, StreamName.Reminder);
                var reminderSubscription = await reminderStream.SubscribeAsync(OnReminder);

                Console.WriteLine("What is your name?");
                var name = Console.ReadLine();
                var helloGrain = clusterClient.GetGrain<IHelloGrain>(Guid.NewGuid());
                Console.WriteLine(await helloGrain.SayHello(name));

                await saidHelloSubscription.UnsubscribeAsync();
                await reminderSubscription.UnsubscribeAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return -1;
            }

            return 0;
        }

        private static Task OnSaidHello(string name, StreamSequenceToken token)
        {
            Console.WriteLine($"{name} said hello.");
            return Task.CompletedTask;
        }

        private static Task OnReminder(string reminder, StreamSequenceToken token)
        {
            Console.WriteLine(reminder);
            return Task.CompletedTask;
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
