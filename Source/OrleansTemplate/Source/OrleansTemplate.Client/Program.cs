namespace OrleansTemplate.Client;

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Streams;
using OrleansTemplate.Abstractions.Constants;
using OrleansTemplate.Abstractions.Grains;

public static class Program
{
    public static async Task<int> Main()
    {
        try
        {
            var host = CreateHostBuilder().Build();
            var task = host.RunAsync().ConfigureAwait(false);

            var clusterClient = host.Services.GetRequiredService<IClusterClient>();

            // Set a trace ID, so that requests can be identified.
            RequestContext.Set("TraceId", Guid.NewGuid());

            var reminderGrain = clusterClient.GetGrain<IReminderGrain>(Guid.Empty);
            await reminderGrain.SetReminderAsync("Don't forget to say hello!").ConfigureAwait(false);

            var streamProvider = clusterClient.GetStreamProvider(StreamProviderName.Default);
            var saidHelloStream = streamProvider.GetStream<string>(StreamId.Create(StreamName.SaidHello, Guid.Empty));
            var saidHelloSubscription = await saidHelloStream.SubscribeAsync(OnSaidHelloAsync).ConfigureAwait(false);
            var reminderStream = streamProvider.GetStream<string>(StreamId.Create(StreamName.Reminder, Guid.Empty));
            var reminderSubscription = await reminderStream.SubscribeAsync(OnReminderAsync).ConfigureAwait(false);

#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("What is your name?");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
            var name = Console.ReadLine() ?? "Rehan";
            var helloGrain = clusterClient.GetGrain<IHelloGrain>(Guid.NewGuid());
            Console.WriteLine(await helloGrain.SayHelloAsync(name).ConfigureAwait(false));

            await saidHelloSubscription.UnsubscribeAsync().ConfigureAwait(false);
            await reminderSubscription.UnsubscribeAsync().ConfigureAwait(false);
        }
        catch (SiloUnavailableException)
        {
            Console.WriteLine("Lost connection to the cluster.");
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception exception)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            Console.WriteLine(exception.ToString());
            return 1;
        }

        return 0;
    }

    private static Task OnSaidHelloAsync(string name, StreamSequenceToken token)
    {
        Console.WriteLine($"{name} said hello.");
        return Task.CompletedTask;
    }

    private static Task OnReminderAsync(string reminder, StreamSequenceToken token)
    {
        Console.WriteLine(reminder);
        return Task.CompletedTask;
    }

    private static IHostBuilder CreateHostBuilder() =>
        new HostBuilder()
            .UseOrleansClient(CreateOrleansClient);

    private static void CreateOrleansClient(IClientBuilder clientBuilder) =>
        clientBuilder
            .UseAzureStorageClustering(options => options.ConfigureTableServiceClient("UseDevelopmentStorage=true"))
            .Configure<ClusterOptions>(
                options =>
                {
                    options.ClusterId = Cluster.ClusterId;
                    options.ServiceId = Cluster.ServiceId;
                })
#if TLS
            .AddBroadcastChannel(StreamProviderName.Default)
            .UseTls(
                options =>
                {
                    // TODO: Configure a certificate.
                    options.LocalCertificate = null;

                    // TODO: Do not allow any remote certificates in production.
                    options.AllowAnyRemoteCertificate();
                });
#else
            .AddBroadcastChannel(StreamProviderName.Default);
#endif

}
