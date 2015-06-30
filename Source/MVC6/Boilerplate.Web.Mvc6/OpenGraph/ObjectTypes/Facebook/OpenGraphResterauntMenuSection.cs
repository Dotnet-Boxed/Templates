namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu_section/
    /// </summary>
    public class OpenGraphResterauntMenuSection : OpenGraphMetadata
    {
        private readonly string menutUrl;

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphResterauntMenuSection" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="menutUrl">The URL to the page about the menu this section is from. This URL must contain profile meta tags <see cref="OpenGraphResterauntMenu"/>.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">location is <c>null</c>.</exception>
        public OpenGraphResterauntMenuSection(string title, OpenGraphImage image, string menutUrl, string url = null)
            : base(title, image, url)
        {
            if (menutUrl == null) { throw new ArgumentNullException("menutUrl"); }

            this.menutUrl = menutUrl;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the URL's to the pages about the menu items. This URL must contain restaurant.menuitem meta tags <see cref="OpenGraphResterauntMenuItem"/>.
        /// </summary>
        public IEnumerable<string> ItemUrls { get; set; }

        /// <summary>
        /// Gets the URL to the page about the menu this section is from. This URL must contain profile meta tags <see cref="OpenGraphResterauntMenu"/>.
        /// </summary>
        public string MenutUrl { get { return this.menutUrl; } }

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

            stringBuilder.AppendMetaPropertyContent("restaurant:menu", this.MenutUrl);
        }

        #endregion
    }
}
