namespace Boilerplate.Web.Mvc.TagHelpers
{
    using Microsoft.AspNet.Mvc.Rendering;

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
        public static HtmlString ReferrerMeta(this IHtmlHelper htmlHelper, ReferrerMode referrerMode)
        {
            if (referrerMode == ReferrerMode.NoneWhenDowngrade)
            {
                return null;
            }
     
            return new HtmlString("<meta name=\"referrer\" content=\"" + referrerMode.ToLowercaseString() + "\">");
        }
    }
}
