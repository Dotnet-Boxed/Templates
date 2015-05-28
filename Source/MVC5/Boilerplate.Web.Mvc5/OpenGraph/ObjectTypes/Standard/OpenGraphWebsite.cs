namespace Boilerplate.Web.Mvc.OpenGraph
{
    /// <summary>
    /// An object representing a website. This object type is part of the Open Graph standard.
    /// </summary>
    public class OpenGraphWebsite : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphWebsite"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        public OpenGraphWebsite(string title, OpenGraphImage image)
            : base(title, image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphWebsite"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphWebsite(string title, OpenGraphImage image, string url)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "website: http://ogp.me/ns/website#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Website; } }

        #endregion
    }
}
