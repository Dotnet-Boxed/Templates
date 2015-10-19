namespace MvcBoilerplate.Settings
{
    /// <summary>
    /// The settings for the current application.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the short name of the application, used for display purposes where the full name will be too long.
        /// </summary>
        public string SiteShortTitle { get; set; }

        /// <summary>
        /// Gets or sets the full name of the application.
        /// </summary>
        public string SiteTitle { get; set; }
    }
}