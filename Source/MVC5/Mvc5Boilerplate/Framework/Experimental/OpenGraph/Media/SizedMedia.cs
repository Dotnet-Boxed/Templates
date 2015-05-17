namespace MvcBoilerplate.Framework.OpenGraph
{
    public class SizedMedia : Media
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SizedMedia"/> class.
        /// </summary>
        /// <param name="mediaUrl">The media URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if mediaUrl is <c>null</c>.</exception>
        public SizedMedia(string mediaUrl)
            : base(mediaUrl)
        {
        }

        /// <summary>
        /// Gets or sets the height of the media in pixels. This is optional.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the media in pixels. This is optional.
        /// </summary>
        public int? Width { get; set; }
    }
}
