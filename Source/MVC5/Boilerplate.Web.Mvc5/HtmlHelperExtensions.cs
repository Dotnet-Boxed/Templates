namespace Boilerplate.Web.Mvc
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// <see cref="HtmlHelper"/> extension methods.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates a string containing the referrer meta tags. <see cref="ReferrerMode"/> for more information.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="referrerMode">The type of referrer allowed to be sent.</param>
        /// <returns>The referrer meta tag.</returns>
        public static IHtmlString ReferrerMeta(this HtmlHelper htmlHelper, ReferrerMode referrerMode)
        {
            if (referrerMode == ReferrerMode.NoneWhenDowngrade)
            {
                return null;
            }

            return new MvcHtmlString("<meta name=\"referrer\" content=\"" + referrerMode.ToLowercaseString() + "\">");
        }
    }
}
