namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System.Text;

    /// <summary>
    /// A audio file that complements this object.
    /// </summary>
    public class OpenGraphAudio : OpenGraphMedia
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphAudio"/> class.
        /// </summary>
        /// <param name="audioUrl">The audio URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if audioUrl is <c>null</c>.</exception>
        public OpenGraphAudio(string audioUrl)
            : base(audioUrl)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            stringBuilder.AppendMeta("og:audio", this.Url);
            stringBuilder.AppendMetaIfNotNull("og:audio:secure_url", this.UrlSecure);
            stringBuilder.AppendMetaIfNotNull("og:audio:type", this.Type);
        }

        #endregion
    }
}
