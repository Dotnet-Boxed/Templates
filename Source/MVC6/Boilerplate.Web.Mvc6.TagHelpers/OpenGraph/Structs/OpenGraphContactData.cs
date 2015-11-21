namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// A set of contact information, including address, phone, email, fax and website.
    /// </summary>
    public class OpenGraphContactData
    {
        private readonly string country;
        private readonly string locality;
        private readonly string postalCode;
        private readonly string streetAddress;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphContactData" /> class.
        /// </summary>
        /// <param name="streetAddress">The number and street of the postal address for this business.</param>
        /// <param name="locality">The city (or locality) line of the postal address for this business.</param>
        /// <param name="postalCode">The postcode (or ZIP code) of the postal address for this business</param>
        /// <param name="country">The country of the postal address for this business.</param>
        /// <exception cref="System.ArgumentNullException">streetAddress or locality or postalCode or country is <c>null.</c>.</exception>
        public OpenGraphContactData(string streetAddress, string locality, string postalCode, string country)
        {
            if (streetAddress == null) { throw new ArgumentNullException(nameof(streetAddress)); }
            if (locality == null) { throw new ArgumentNullException(nameof(locality)); }
            if (postalCode == null) { throw new ArgumentNullException(nameof(postalCode)); }
            if (country == null) { throw new ArgumentNullException(nameof(country)); }

            this.country = country;
            this.locality = locality;
            this.postalCode = postalCode;
            this.streetAddress = streetAddress;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the country of the postal address for this business.
        /// </summary>
        public string Country { get { return this.country; } }

        /// <summary>
        /// Gets or sets the email address to contact this business.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a fax number to contact this business.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Gets the city (or locality) line of the postal address for this business.
        /// </summary>
        public string Locality { get { return this.locality; } }

        /// <summary>
        /// Gets or sets a telephone number to contact this business.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets the postcode (or ZIP code) of the postal address for this business.
        /// </summary>
        public string PostalCode { get { return this.postalCode; } }

        /// <summary>
        /// Gets or sets the state (or region) line of the postal address for this business.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets the number and street of the postal address for this business.
        /// </summary>
        public string StreetAddress { get { return this.streetAddress; } }

        /// <summary>
        /// Gets or sets a website for this business.
        /// </summary>
        public string Website { get; set; }

        #endregion
    }
}
