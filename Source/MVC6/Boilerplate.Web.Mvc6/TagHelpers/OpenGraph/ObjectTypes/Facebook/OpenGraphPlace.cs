namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a place - such as a venue, a business, a landmark, or any other location which can be identified by longitude and 
    /// latitude. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/place/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-place", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + LocationAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphPlace : OpenGraphMetadata
    {
        #region Constants

        private const string LocationAttributeName = "location";

        #endregion
        
        #region Public Properties

        /// <summary>
        /// Gets or sets the location of the place.
        /// </summary>
        [HtmlAttributeName(LocationAttributeName)]
        public OpenGraphLocation Location { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "place: http://ogp.me/ns/place#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Place; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("place:location:latitude", this.Location.Latitude);
            stringBuilder.AppendMetaPropertyContent("place:location:longitude", this.Location.Longitude);
            stringBuilder.AppendMetaPropertyContentIfNotNull("place:location:altitude", this.Location.Altitude);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.Location == null) { throw new ArgumentNullException(nameof(this.Location)); }
        }

        #endregion
    }
}
