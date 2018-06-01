namespace ApiTemplate.Options
{
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        public CacheProfileOptions CacheProfiles { get; set; }

        public CompressionOptions Compression { get; set; }

        public KestrelServerOptions Kestrel { get; set; }
    }
}
