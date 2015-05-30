namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System.Text;

    /// <summary>
    /// A video file that complements this object.
    /// </summary>
    public class OpenGraphVideo : OpenGraphSizedMedia
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphVideo"/> class.
        /// </summary>
        /// <param name="videoUrl">The video URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if videoUrl is <c>null</c>.</exception>
        public OpenGraphVideo(string videoUrl)
            : base(videoUrl)
        {
        }

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.AppendMetaPropertyContent("og:video", this.Url);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:video:secure_url", this.UrlSecure);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:video:type", this.Type);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:video:width", this.Width);
            stringBuilder.AppendMetaPropertyContentIfNotNull("og:video:height", this.Height);
        }

        #endregion
    }
}
