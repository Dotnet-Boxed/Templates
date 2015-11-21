namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a restaurant at a specific location. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.restaurant/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-restaurant", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + LocationAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphRestaurant : OpenGraphMetadata
    {
        #region Constants

        private const string CategoriesAttributeName = "categories";
        private const string ContactInfoAttributeName = "contact-info";
        private const string LocationAttributeName = "location";
        private const string MenuUrlsAttributeName = "menu-urls";
        private const string PriceRatingAttributeName = "price-rating";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a collection of categories describing this restaurant's food.
        /// </summary>
        [HtmlAttributeName(CategoriesAttributeName)]
        public IEnumerable<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the contact information for the restaurant.
        /// </summary>
        [HtmlAttributeName(ContactInfoAttributeName)]
        public OpenGraphContactData ContactInfo { get; set; }

        /// <summary>
        /// Gets or sets the location of the place.
        /// </summary>
        [HtmlAttributeName(LocationAttributeName)]
        public OpenGraphLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the menus. This URL must contain restaurant.menu meta tags <see cref="OpenGraphResterauntMenu"/>.
        /// </summary>
        [HtmlAttributeName(MenuUrlsAttributeName)]
        public IEnumerable<string> MenuUrls { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "restaurant: http://ogp.me/ns/restaurant# place: http://ogp.me/ns/place#"; } }

        /// <summary>
        /// Gets or sets the price rating for this restaurant (from 1 to 4).
        /// </summary>
        [HtmlAttributeName(PriceRatingAttributeName)]
        public int? PriceRating { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Restaurant; } }

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

            if (this.Categories != null)
            {
                foreach (string category in this.Categories)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:category", category);
                }
            }

            if (this.ContactInfo != null)
            {
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:street_address", this.ContactInfo.StreetAddress);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:locality", this.ContactInfo.Locality);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:region", this.ContactInfo.Region);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:postal_code", this.ContactInfo.PostalCode);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:country_name", this.ContactInfo.Country);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:email", this.ContactInfo.Email);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:phone_number", this.ContactInfo.Phone);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:fax_number", this.ContactInfo.Fax);
                stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:contact_data:website", this.ContactInfo.Website);
            }

            if (this.MenuUrls != null)
            {
                foreach (string menuUrl in this.MenuUrls)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:menu", menuUrl);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("restaurant:price_rating", this.PriceRating);
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
