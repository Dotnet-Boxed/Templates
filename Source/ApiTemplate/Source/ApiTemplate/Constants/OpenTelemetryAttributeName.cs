namespace ApiTemplate.Constants;

/// <summary>
/// Constants for semantic attribute names outlined by the OpenTelemetry specifications.
/// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/README.md"/>.
/// </summary>
public static class OpenTelemetryAttributeName
{
    /// <summary>
    /// Constants for deployment semantic attribute names outlined by the OpenTelemetry specifications.
    /// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/11cc73939a32e3a2e6f11bdeab843c61cf8594e9/specification/resource/semantic_conventions/deployment_environment.md"/>.
    /// </summary>
    public static class Deployment
    {
        /// <summary>
        /// The name of the deployment environment (aka deployment tier).
        /// </summary>
        /// <example>staging; production.</example>
        public const string Environment = "deployment.environment";
    }

    /// <summary>
    /// Constants for end user semantic attribute names outlined by the OpenTelemetry specifications.
    /// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/span-general.md"/>.
    /// </summary>
    public static class EndUser
    {
        /// <summary>
        /// Username or client_id extracted from the access token or Authorization header in the inbound request from outside the system.
        /// </summary>
        /// <example>E.g. username.</example>
        public const string Id = "enduser.id";

        /// <summary>
        /// Actual/assumed role the client is making the request under extracted from token or application security context.
        /// </summary>
        /// <example>E.g. admin.</example>
        public const string Role = "enduser.role";

        /// <summary>
        /// Scopes or granted authorities the client currently possesses extracted from token or application security context.
        /// The value would come from the scope associated with an OAuth 2.0 Access Token or an attribute value in a SAML 2.0 Assertion.
        /// </summary>
        /// <example>E.g. read:message,write:files.</example>
        public const string Scope = "enduser.scope";
    }

    /// <summary>
    /// Constants for HTTP semantic attribute names outlined by the OpenTelemetry specifications.
    /// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/http.md"/>.
    /// </summary>
    public static class Http
    {
        /// <summary>
        /// The URI scheme identifying the used protocol.
        /// </summary>
        /// <example>E.g. http or https.</example>
        public const string Scheme = "http.scheme";

        /// <summary>
        /// Kind of HTTP protocol used.
        /// </summary>
        /// <example>E.g. 1.0, 1.1, 2.0, SPDY or QUIC.</example>
        public const string Flavor = "http.flavor";

        /// <summary>
        /// The IP address of the original client behind all proxies, if known (e.g. from X-Forwarded-For).
        /// </summary>
        /// <example>E.g. 83.164.160.102.</example>
        public const string ClientIP = "http.client_ip";

        /// <summary>
        /// The size of the request payload body in bytes. This is the number of bytes transferred excluding headers and is often,
        /// but not always, present as the Content-Length header. For requests using transport encoding, this should be the
        /// compressed size.
        /// </summary>
        /// <example>E.g. 3495.</example>
        public const string RequestContentLength = "http.request_content_length";

        /// <summary>
        /// The content type of the request body.
        /// </summary>
        /// <example>E.g. application/json.</example>
        public const string RequestContentType = "http.request_content_type";

        /// <summary>
        /// The size of the response payload body in bytes. This is the number of bytes transferred excluding headers and is often,
        /// but not always, present as the Content-Length header. For requests using transport encoding, this should be the
        /// compressed size.
        /// </summary>
        /// <example>E.g. 3495.</example>
        public const string ResponseContentLength = "http.response_content_length";

        /// <summary>
        /// The content type of the response body.
        /// </summary>
        /// <example>E.g. application/json.</example>
        public const string ResponseContentType = "http.response_content_type";
    }

    /// <summary>
    /// Constants for host semantic attribute names outlined by the OpenTelemetry specifications.
    /// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/11cc73939a32e3a2e6f11bdeab843c61cf8594e9/specification/resource/semantic_conventions/host.md"/>.
    /// </summary>
    public static class Host
    {
        /// <summary>
        /// Name of the host. On Unix systems, it may contain what the hostname command returns, or the fully qualified hostname,
        /// or another name specified by the user.
        /// </summary>
        /// <example>E.g. opentelemetry-test.</example>
        public const string Name = "host.name";
    }

    /// <summary>
    /// Constants for service semantic attribute names outlined by the OpenTelemetry specifications.
    /// <see href="https://github.com/open-telemetry/opentelemetry-specification/blob/master/specification/trace/semantic_conventions/messaging.md"/>.
    /// </summary>
    public static class Service
    {
        /// <summary>
        /// The name of the service sending messages.
        /// </summary>
        public const string Name = "service.name";
    }
}
