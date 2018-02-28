namespace ApiTemplate.Options
{
    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
        public CacheProfileOptions CacheProfiles { get; set; }

        public CompressionOptions Compression { get; set; }

        public KestrelOptions Kestrel { get; set; }
    }
}
