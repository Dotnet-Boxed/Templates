namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// An object representing a website. This object type is part of the Open Graph standard.
    /// See http://ogp.me/
    /// </summary>
    [TargetElement(Attributes = nameof(Title) + "," + nameof(MainImage), TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphWebsite : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphWebsite"/> class.
        /// </summary>
        public OpenGraphWebsite() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphWebsite"/> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphWebsite(string title, OpenGraphImage mainImage, string url = null)
            : base(title, mainImage, url)
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
