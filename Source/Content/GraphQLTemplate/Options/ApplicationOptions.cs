namespace GraphQLTemplate.Options
{
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
        public KestrelServerOptions Kestrel { get; set; }
    }
}
