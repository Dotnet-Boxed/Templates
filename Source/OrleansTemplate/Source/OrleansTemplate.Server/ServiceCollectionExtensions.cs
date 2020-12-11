namespace OrleansTemplate.Server
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.DependencyInjection;
    using OpenTelemetry.Trace;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Open Telemetry services and configures instrumentation and exporters.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services with open telemetry added.</returns>
        public static IServiceCollection AddCustomOpenTelemetryTracing(this IServiceCollection services) =>
            services.AddOpenTelemetryTracing(
                builder =>
                {
                    builder
                        .AddAspNetCoreInstrumentation(
                            options =>
                            {
                                // Enrich spans with additional request and response meta data.
                                // See https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md
                                options.Enrich = (activity, eventName, obj) =>
                                {
                                    if (obj is HttpRequest request)
                                    {
                                        activity.AddTag("http.flavor", GetHttpFlavour(request.Protocol));
                                        activity.AddTag("http.scheme", request.Scheme);
                                        activity.AddTag("http.client_ip", request.Headers[ForwardedHeadersDefaults.XForwardedForHeaderName]);
                                        activity.AddTag("http.request_content_length", request.ContentLength);
                                        activity.AddTag("http.request_content_type", request.ContentType);
                                    }
                                    else if (obj is HttpResponse response)
                                    {
                                        activity.AddTag("http.response_content_length", response.ContentLength);
                                        activity.AddTag("http.response_content_type", response.ContentType);
                                    }

                                    static string GetHttpFlavour(string protocol)
                                    {
                                        if (HttpProtocol.IsHttp10(protocol))
                                        {
                                            return "1.0";
                                        }
                                        else if (HttpProtocol.IsHttp11(protocol))
                                        {
                                            return "1.1";
                                        }
                                        else if (HttpProtocol.IsHttp2(protocol))
                                        {
                                            return "2.0";
                                        }
                                        else if (HttpProtocol.IsHttp3(protocol))
                                        {
                                            return "3.0";
                                        }

                                        throw new InvalidOperationException($"Protocol {protocol} not recognised.");
                                    }
                                };
                                options.RecordException = true;
                            });
                    // TODO: Add OpenTelemetry.Instrumentation.* NuGet packages and configure them to collect more span data.
                    //       E.g. add OpenTelemetry.Instrumentation.Http to instrument calls to HttpClient.
                    // TODO: Add OpenTelemetry.Exporter.* NuGet packages and configure them here to export open telemetry span data.
                    //       E.g. Add OpenTelemetry.Exporter.Jaeger to export span data to Jaeger.
                });
    }
}
