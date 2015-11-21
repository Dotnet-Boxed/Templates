namespace Boilerplate.Web.Mvc.TagHelpers
{
    /// <summary>
    /// <see cref="ReferrerMode"/> extension methods.
    /// </summary>
    internal static class ReferrerModeExtensions
    {
        /// <summary>
        /// Returns the lower-case <see cref="string" /> representation of the <see cref="ReferrerMode" />.
        /// </summary>
        /// <param name="referrerMode">The referrer mode.</param>
        /// <returns>
        /// The lower-case <see cref="string" /> representation of the <see cref="ReferrerMode" />.
        /// </returns>
        public static string ToLowercaseString(this ReferrerMode referrerMode)
        {
            switch (referrerMode)
            {
                case ReferrerMode.None:
                    return "none";
                case ReferrerMode.NoneWhenDowngrade:
                    return "none-when-downgrade";
                case ReferrerMode.Origin:
                    return "origin";
                case ReferrerMode.OriginWhenCrossOrigin:
                    return "origin-when-crossorigin";
                case ReferrerMode.UnsafeUrl:
                    return "unsafe-url";
                default:
                    return string.Empty;
            }
        }
    }
}
