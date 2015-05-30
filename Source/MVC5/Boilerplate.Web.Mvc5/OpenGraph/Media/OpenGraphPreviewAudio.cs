namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System.Text;

    /// <summary>
    /// A audio file that complements this object. Only to be used with <see cref="OpenGraphMusicSong"/>.
    /// </summary>
    public class OpenGraphPreviewAudio : OpenGraphMedia
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphPreviewAudio"/> class.
        /// </summary>
        /// <param name="audioUrl">The audio URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if audioUrl is <c>null</c>.</exception>
        public OpenGraphPreviewAudio(string audioUrl)
            : base(audioUrl)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.AppendMetaPropertyContent("music:preview_url:url", this.Url);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:preview_url:secure_url", this.UrlSecure);
            stringBuilder.AppendMetaPropertyContentIfNotNull("music:preview_url:type", this.Type);
        }

        #endregion
    }
}
