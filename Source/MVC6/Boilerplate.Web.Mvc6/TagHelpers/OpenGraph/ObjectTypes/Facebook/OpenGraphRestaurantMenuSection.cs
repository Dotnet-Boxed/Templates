namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu_section/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-restaurant-menu-section", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + MenuUrlAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphRestaurantMenuSection : OpenGraphMetadata
    {
        #region Constants

        private const string ItemUrlsAttributeName = "item-urls";
        private const string MenuUrlAttributeName = "menu-url";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the menu items. This URL must contain restaurant.menuitem meta tags <see cref="OpenGraphResterauntMenuItem"/>.
        /// </summary>
        [HtmlAttributeName(ItemUrlsAttributeName)]
        public IEnumerable<string> ItemUrls { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the menu this section is from. This URL must contain profile meta tags <see cref="OpenGraphResterauntMenu"/>.
        /// </summary>
        [HtmlAttributeName(MenuUrlAttributeName)]
        public string MenuUrl { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "restaurant: http://ogp.me/ns/restaurant#"; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.RestaurantMenuSection; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            if (this.ItemUrls != null)
            {
                foreach (string itemUrl in this.ItemUrls)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:item", itemUrl);
                }
            }

            stringBuilder.AppendMetaPropertyContent("restaurant:menu", this.MenuUrl);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.MenuUrl == null) { throw new ArgumentNullException(nameof(this.MenuUrl)); }
        }

        #endregion
    }
}
