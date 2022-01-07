namespace GraphQLTemplate.ConfigureOptions;

using System.IO.Compression;
using GraphQLTemplate.Options;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;

/// <summary>
/// Configures dynamic GZIP and Brotli response compression. This is turned off for HTTPS requests by default to avoid
/// the BREACH security vulnerability.
/// </summary>
public class ConfigureResponseCompressionOptions :
    IConfigureOptions<ResponseCompressionOptions>,
    IConfigureOptions<BrotliCompressionProviderOptions>,
    IConfigureOptions<GzipCompressionProviderOptions>
{
    private readonly CompressionOptions compressionOptions;

    public ConfigureResponseCompressionOptions(CompressionOptions compressionOptions) =>
        this.compressionOptions = compressionOptions;

    public void Configure(ResponseCompressionOptions options)
    {
        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
        var customMimeTypes = this.compressionOptions?.MimeTypes ?? Enumerable.Empty<string>();
        options.MimeTypes = customMimeTypes.Concat(ResponseCompressionDefaults.MimeTypes);

        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    }

    public void Configure(BrotliCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;

    public void Configure(GzipCompressionProviderOptions options) => options.Level = CompressionLevel.Optimal;
}
