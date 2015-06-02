namespace Boilerplate.Web.Mvc.Twitter
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Creates Twitter card meta tags. <see cref="TwitterCard"/> for more information.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a string containing the Twitter card meta tags. <see cref="TwitterCard"/> for more information.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="twitterCard">The Twitter card metadata.</param>
        /// <returns>The meta tags.</returns>
        public static IHtmlString TwitterCard(this HtmlHelper htmlHelper, TwitterCard twitterCard)
        {
            return twitterCard;
        }
    }
}
