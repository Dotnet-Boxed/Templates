namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a place of business that has a location, operating hours and contact information. This object type is not part of 
    /// the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/business.business/
    /// </summary>
    public class OpenGraphBusiness : OpenGraphMetadata
    {
        private const string TimeOfDayFormat = "hh:mm";

        private readonly OpenGraphContactData contactData;
        private readonly OpenGraphLocation location;

        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphBusiness" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="contactData">The contact data for the business.</param>
        /// <param name="location">The location of the business.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">contactData or location is <c>null</c>.</exception>
        public OpenGraphBusiness(string title, OpenGraphImage image, OpenGraphContactData contactData, OpenGraphLocation location, string url = null)
            : base(title, image, url)
        {
            if (contactData == null)
            {
                throw new ArgumentNullException("contactData");
            }

            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            this.location = location;
            this.contactData = contactData;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the contact data for the business.
        /// </summary>
        public OpenGraphContactData ContactData { get { return this.contactData; } }

        /// <summary>
        /// Gets the location of the business.
        /// </summary>
        public OpenGraphLocation Location { get { return this.location; } }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "business: http://ogp.me/ns/business# place: http://ogp.me/ns/place#"; } }

        /// <summary>
        /// Gets or sets the opening hours of the business.
        /// </summary>
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
    }
}
