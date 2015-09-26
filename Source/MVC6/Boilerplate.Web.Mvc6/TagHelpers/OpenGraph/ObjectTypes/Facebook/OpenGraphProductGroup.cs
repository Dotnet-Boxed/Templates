namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System.Text;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// This object type represents a group of product items. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product.group/
    /// </summary>
    [TargetElement(Attributes = nameof(Title) + "," + nameof(MainImage), TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphProductGroup : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProductGroup" /> class.
        /// </summary>
        public OpenGraphProductGroup() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProductGroup" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphProductGroup(string title, OpenGraphImage mainImage, string url = null)
            : base(title, mainImage, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "product: http://ogp.me/ns/product#"; } }

        /// <summary>
        /// Gets or sets the retailer's ID for the product group.
        /// </summary>
        public string RetailerGroupId { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.ProductGroup; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_group_id", this.RetailerGroupId);
        }

        #endregion
    }
}