namespace GraphQLTemplate.Options
{
    using System.ComponentModel.DataAnnotations;
    using GraphQL.Server;
#if ForwardedHeaders
    using Microsoft.AspNetCore.Builder;
#elif HostFiltering
    using Microsoft.AspNetCore.HostFiltering;
#endif
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        [Required]
        public CacheProfileOptions CacheProfiles { get; set; }

#if ResponseCompression
        public CompressionOptions Compression { get; set; }

#endif
#if ForwardedHeaders
        [Required]
        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

#elif HostFiltering
        [Required]
        public HostFilteringOptions HostFiltering { get; set; }

#endif
        [Required]
        public GraphQLOptions GraphQL { get; set; }

        [Required]
        public KestrelServerOptions Kestrel { get; set; }
    }
}
