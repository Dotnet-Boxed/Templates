namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-restaurant-menu", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + RestaurantUrlAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphRestaurantMenu : OpenGraphMetadata
    {
        #region Constants

        private const string RestaurantUrlAttributeName = "restaurant-url";
        private const string SectionUrlsAttributeName = "section-urls";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "restaurant: http://ogp.me/ns/restaurant#"; } }

        /// <summary>
        /// Gets or sets the URL to the page about the restaurant who wrote the menu. This URL must contain profile meta tags <see cref="OpenGraphResteraunt"/>.
        /// </summary>
        [HtmlAttributeName(RestaurantUrlAttributeName)]
        public string RestaurantUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the menu sections. This URL must contain restaurant.section meta tags <see cref="OpenGraphResterauntMenuSection"/>.
        /// </summary>
        [HtmlAttributeName(SectionUrlsAttributeName)]
        public IEnumerable<string> SectionUrls { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.RestaurantMenu; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("restaurant:restaurant", this.RestaurantUrl);

            if (this.SectionUrls != null)
            {
                foreach (string sectionUrl in this.SectionUrls)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:section", sectionUrl);
                }
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.RestaurantUrl == null) { throw new ArgumentNullException(nameof(this.RestaurantUrl)); }
        }

        #endregion
    }
}
