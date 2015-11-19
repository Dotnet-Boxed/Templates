namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a place of business that has a location, operating hours and contact information. This object type is not part of 
    /// the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/business.business/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-business", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + ContactDataAttributeName + "," + LocationAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphBusiness : OpenGraphMetadata
    {
        #region Constants

        private const string ContactDataAttributeName = "contact-data";
        private const string LocationAttributeName = "location";
        private const string OpeningHoursAttributeName = "opening-hours";

        private const string TimeOfDayFormat = "hh:mm"; 

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the contact data for the business.
        /// </summary>
        [HtmlAttributeName(ContactDataAttributeName)]
        public OpenGraphContactData ContactData { get; set; }

        /// <summary>
        /// Gets or sets the location of the business.
        /// </summary>
        [HtmlAttributeName(LocationAttributeName)]
        public OpenGraphLocation Location { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "business: http://ogp.me/ns/business# place: http://ogp.me/ns/place#"; } }

        /// <summary>
        /// Gets or sets the opening hours of the business.
        /// </summary>
        [HtmlAttributeName(OpeningHoursAttributeName)]
        public IEnumerable<OpenGraphHours> OpeningHours { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Business; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:street_address", this.ContactData.StreetAddress);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:locality", this.ContactData.Locality);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:region", this.ContactData.Region);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:postal_code", this.ContactData.PostalCode);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:country_name", this.ContactData.Country);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:email", this.ContactData.Email);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:phone_number", this.ContactData.Phone);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:fax_number", this.ContactData.Fax);
            stringBuilder.AppendMetaPropertyContentIfNotNull("business:contact_data:website", this.ContactData.Website);

            if (this.OpeningHours != null)
            {
                foreach (OpenGraphHours hours in this.OpeningHours)
                {
                    stringBuilder.AppendMetaPropertyContent("business:hours:day", hours.Day.ToLowercaseString());
                    stringBuilder.AppendMetaPropertyContent("business:hours:start", hours.Start.ToString(TimeOfDayFormat));
                    stringBuilder.AppendMetaPropertyContent("business:hours:end", hours.End.ToString(TimeOfDayFormat));
                }
            }

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

            if (this.ContactData == null) { throw new ArgumentNullException(nameof(this.ContactData)); }
            if (this.Location == null) { throw new ArgumentNullException(nameof(this.Location)); }
        }

        #endregion
    }
}
