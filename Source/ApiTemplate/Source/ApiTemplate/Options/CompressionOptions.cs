namespace ApiTemplate.Options;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// The dynamic response compression options for the application.
/// </summary>
public class CompressionOptions
{
    public CompressionOptions() => this.MimeTypes = new List<string>();

    /// <summary>
    /// Gets a list of MIME types to be compressed in addition to the default set used by ASP.NET Core.
    /// </summary>
    [Required]
    public List<string> MimeTypes { get; }
}
