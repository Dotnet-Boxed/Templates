namespace Boilerplate.Web.Mvc.TagHelpers.Twitter
{
    using Microsoft.AspNet.Mvc.Rendering;

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
        public static HtmlString TwitterCard(this IHtmlHelper htmlHelper, TwitterCard twitterCard)
        {
            return new HtmlString(twitterCard.ToString());
        }
    }
}
