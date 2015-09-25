namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a single item on a restaurant's menu. Every item belongs within a menu section.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu_item/
    /// </summary>
    public class OpenGraphRestaurantMenuItem : OpenGraphMetadata
    {
        private readonly string sectionUrl;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphRestaurantMenuItem" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="sectionUrl">The URL to the page about the section this menu item is from. This URL must contain profile meta tags <see cref="OpenGraphResterauntMenuSection"/>.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">location is <c>null</c>.</exception>
        public OpenGraphRestaurantMenuItem(string title, OpenGraphImage image, string sectionUrl, string url = null)
            : base(title, image, url)
        {
            if (sectionUrl == null) { throw new ArgumentNullException("sectionUrl"); }

            this.sectionUrl = sectionUrl;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "restaurant: http://ogp.me/ns/restaurant#"; } }

        /// <summary>
        /// Gets the URL to the page about the section this menu item is from. This URL must contain profile meta tags <see cref="OpenGraphResterauntMenuSection"/>.
        /// </summary>
        public string SectionUrl { get { return this.sectionUrl; } }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.RestaurantMenuItem; } }

        /// <summary>
        /// Gets or sets the variations of this menu item.
        /// </summary>
        public IEnumerable<OpenGraphMenuItemVariation> Variations { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("restaurant:section", this.SectionUrl);

            if (this.Variations != null)
            {
                foreach (OpenGraphMenuItemVariation variation in this.Variations)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:variation:name", variation.Name);
                    stringBuilder.AppendMetaPropertyContent("restaurant:variation:price:amount", variation.Price.Amount);
                    stringBuilder.AppendMetaPropertyContent("restaurant:variation:price:currency", variation.Price.Currency);
                }
            }
        }

        #endregion
    }
}
