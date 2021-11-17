namespace GraphQLTemplate.Options;

using System.ComponentModel.DataAnnotations;
#if (!ForwardedHeaders && HostFiltering)
using Microsoft.AspNetCore.HostFiltering;
#endif
using Microsoft.AspNetCore.Server.Kestrel.Core;

/// <summary>
/// All options for the application.
/// </summary>
public class ApplicationOptions
{
    public ApplicationOptions() => this.CacheProfiles = new CacheProfileOptions();

    [Required]
    public CacheProfileOptions CacheProfiles { get; }

#if ResponseCompression
    [Required]
    public CompressionOptions Compression { get; set; } = default!;

#endif
#if ForwardedHeaders
    [Required]
    public ForwardedHeadersOptions ForwardedHeaders { get; set; } = default!;

#elif HostFiltering
    [Required]
    public HostFilteringOptions HostFiltering { get; set; } = default!;

#endif
    [Required]
    public GraphQLOptions GraphQL { get; set; } = default!;

    [Required]
    public HostOptions Host { get; set; } = default!;

    [Required]
    public KestrelServerOptions Kestrel { get; set; } = default!;
#if Redis

    [Required]
    public RedisOptions Redis { get; set; } = default!;
#endif
}
