namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    /// <summary>
    /// <see cref="TwitterCardType"/> extension methods.
    /// </summary>
    internal static class TwitterCardTypeExtensions
    {
        /// <summary>
        /// Returns the Twitter specific <see cref="string" /> representation of the <see cref="TwitterCardType" />.
        /// </summary>
        /// <param name="twitterCardType">Type of the twitter card.</param>
        /// <returns>
        /// The Twitter specific <see cref="string" /> representation of the <see cref="TwitterCardType" />.
        /// </returns>
        public static string ToTwitterString(this TwitterCardType twitterCardType)
        {
            switch (twitterCardType)
            {
                case TwitterCardType.App:
                    return "app";
                case TwitterCardType.Gallery:
                    return "gallery";
                case TwitterCardType.Photo:
                    return "photo";
                case TwitterCardType.Player:
                    return "player";
                case TwitterCardType.Product:
                    return "product";
                case TwitterCardType.Summary:
                    return "summary";
                case TwitterCardType.SummaryLargeImage:
                    return "summary_large_image";
                default:
                    return string.Empty;
            }
        }
    }
}
