namespace MvcBoilerplate.Services
{
    using Boilerplate.Web.Mvc;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.OptionsModel;
    using MvcBoilerplate.Constants;
    using MvcBoilerplate.Settings;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ManifestService : IManifestService
    {
        private readonly IOptions<AppSettings> appSettings;
        private readonly IUrlHelper urlHelper;

        public ManifestService(
            IOptions<AppSettings> appSettings,
            IUrlHelper urlHelper)
        {
            this.appSettings = appSettings;
            this.urlHelper = urlHelper;
        }

        /// <summary>
        /// Gets the manifest JSON for the current site. This allows you to customize the icon and other browser 
        /// settings for Chrome/Android and FireFox (FireFox support is coming). See https://w3c.github.io/manifest/
        /// for the official W3C specification. See http://html5doctor.com/web-manifest-specification/ for more 
        /// information. See https://developer.chrome.com/multidevice/android/installtohomescreen for Chrome's 
        /// implementation.
        /// </summary>
        /// <returns>The manifest JSON for the current site.</returns>
        public string GetManifestJson()
        {
            JObject document = new JObject(
                new JProperty("short_name", this.appSettings.Value.SiteShortTitle),
                new JProperty("name", this.appSettings.Value.SiteTitle),
                new JProperty("icons",
                    new JArray(
                        GetIconJObject("~/img/icons/android-chrome-36x36.png", "36x36", "image/png", "0.75"),
                        GetIconJObject("~/img/icons/android-chrome-48x48.png", "48x48", "image/png", "1.0"),
                        GetIconJObject("~/img/icons/android-chrome-72x72.png", "72x72", "image/png", "1.5"),
                        GetIconJObject("~/img/icons/android-chrome-96x96.png", "96x96", "image/png", "2.0"),
                        GetIconJObject("~/img/icons/android-chrome-144x144.png", "144x144", "image/png", "3.0"),
                        GetIconJObject("~/img/icons/android-chrome-192x192.png", "192x192", "image/png", "4.0"))));

            return document.ToString(Formatting.Indented);
        }

        /// <summary>
        /// Gets a <see cref="JObject"/> containing the specified image details.
        /// </summary>
        /// <param name="iconPath">The path to the icon image.</param>
        /// <param name="sizes">The size of the image in the format AxB.</param>
        /// <param name="type">The MIME type of the image.</param>
        /// <param name="density">The pixel density of the image.</param>
        /// <returns>A <see cref="JObject"/> containing the image details.</returns>
        private JObject GetIconJObject(string iconPath, string sizes, string type, string density)
        {
            return new JObject(
                new JProperty("src", this.urlHelper.Content(iconPath)),
                new JProperty("sizes", sizes),
                new JProperty("type", type),
                new JProperty("density", density));
        }
    }
}
