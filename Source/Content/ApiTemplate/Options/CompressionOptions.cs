namespace ApiTemplate.Options
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The dynamic response compression options for the application.
    /// </summary>
    public class CompressionOptions
    {
        /// <summary>
        /// Gets or sets a list of MIME types to be compressed in addition to the default set used by ASP.NET Core.
        /// </summary>
        [Required]
        public string[] MimeTypes { get; set; }
    }
}
