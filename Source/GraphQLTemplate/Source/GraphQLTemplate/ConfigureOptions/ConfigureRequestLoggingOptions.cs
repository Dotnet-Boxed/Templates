namespace GraphQLTemplate.ConfigureOptions;

using Microsoft.Extensions.Options;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Events;

/// <summary>
/// Configures serilog HTTP request logging. Adds additional properties to each log.
/// See https://github.com/serilog/serilog-aspnetcore.
/// </summary>
public class ConfigureRequestLoggingOptions : IConfigureOptions<RequestLoggingOptions>
{
    private const string MessageTemplate = "{Protocol} {RequestMethod} {RequestPath} responded {StatusCode} {ContentType} in {Elapsed:0.0000} ms";

    private const string HostPropertyName = "Host";
    private const string ProtocolPropertyName = "Protocol";
    private const string SchemePropertyName = "Scheme";
    private const string QueryStringPropertyName = "QueryString";
    private const string EndpointNamePropertyName = "EndpointName";
    private const string ContentTypePropertyName = "ContentType";
    private const string ClientNamePropertyName = "ClientName";
    private const string ClientVersionPropertyName = "ClientVersion";

    private const string ClientNameHttpHeader = "apollographql-client-name";
    private const string ClientVersionHttpHeader = "apollographql-client-version";

    private const string HealthCheckEndpointDisplayName = "Health checks";

    public void Configure(RequestLoggingOptions options)
    {
        options.EnrichDiagnosticContext = EnrichDiagnosticContext;
#if HealthCheck
        options.GetLevel = GetLevel;
#endif
        options.MessageTemplate = MessageTemplate;
    }

    private static void EnrichDiagnosticContext(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        var request = httpContext.Request;
        var response = httpContext.Response;

        diagnosticContext.Set(HostPropertyName, request.Host);
        diagnosticContext.Set(ProtocolPropertyName, request.Protocol);
        diagnosticContext.Set(SchemePropertyName, request.Scheme);

        var queryString = request.QueryString;
        if (queryString.HasValue)
        {
            diagnosticContext.Set(QueryStringPropertyName, queryString.Value);
        }

        var clientName = request.Headers[ClientNameHttpHeader];
        if (clientName.Any())
        {
            diagnosticContext.Set(ClientNamePropertyName, clientName);
        }

        var clientVersion = request.Headers[ClientVersionHttpHeader];
        if (clientVersion.Any())
        {
            diagnosticContext.Set(ClientVersionPropertyName, clientVersion);
        }

        var endpoint = httpContext.GetEndpoint();
        if (endpoint is not null)
        {
            diagnosticContext.Set(EndpointNamePropertyName, endpoint.DisplayName);
        }

        diagnosticContext.Set(ContentTypePropertyName, response.ContentType);
    }
#if HealthCheck

    private static LogEventLevel GetLevel(HttpContext httpContext, double elapsedMilliseconds, Exception exception)
    {
        if (exception is null && httpContext.Response.StatusCode <= 499)
        {
            if (IsHealthCheckEndpoint(httpContext))
            {
                // Health check endpoints are called frequently, so mark them as verbose to avoid filling up the logs.
                return LogEventLevel.Verbose;
            }

            return LogEventLevel.Information;
        }

        return LogEventLevel.Error;
    }

    private static bool IsHealthCheckEndpoint(HttpContext httpContext)
    {
        var endpoint = httpContext.GetEndpoint();
        if (endpoint is not null)
        {
            return string.Equals(endpoint.DisplayName, HealthCheckEndpointDisplayName, StringComparison.Ordinal);
        }

        return false;
    }
#endif
}
