namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// A location specified by latitude and longitude.
    /// </summary>
    public class OpenGraphLocation
    {
        private readonly double? altitude;
        private readonly double latitude;
        private readonly double longitude;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphLocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphLocation"/> class.
        /// </summary>
        /// <param name="latitude">The latitude of the location.</param>
        /// <param name="longitude">The longitude of the location.</param>
        /// <param name="altitude">The altitude of the location.</param>
        public OpenGraphLocation(double latitude, double longitude, double altitude)
            : this(latitude, longitude)
        {
            this.altitude = altitude;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the altitude of the location.
        /// </summary>
        public double? Altitude { get { return this.altitude; } }

        /// <summary>
        /// Gets the latitude of the location.
        /// </summary>
        public double Latitude { get { return this.latitude; } }

        /// <summary>
        /// Gets the longitude of the location.
        /// </summary>
        public double Longitude { get { return this.longitude; } }

        #endregion
    }
}
