namespace ApiTemplate.Options
{
#if (ForwardedHeaders)
    using Microsoft.AspNetCore.Builder;
#endif
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        public CacheProfileOptions CacheProfiles { get; set; }

#if (ResponseCompression)
        public CompressionOptions Compression { get; set; }

#endif
#if (ForwardedHeaders)
        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

#endif
        public KestrelServerOptions Kestrel { get; set; }
    }
}
