namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// <see cref="OpenGraphGender"/> extension methods.
    /// </summary>
    internal static class OpenGraphGenderExtensions
    {
        /// <summary>
        /// Returns the lowercase <see cref="string" /> representation of the <see cref="OpenGraphGender" />.
        /// </summary>
        /// <param name="gender">The gender.</param>
        /// <returns>
        /// The lowercase <see cref="string" /> representation of the <see cref="OpenGraphGender" />.
        /// </returns>
        public static string ToLowercaseString(this OpenGraphGender gender)
        {
            switch (gender)
            {
                case OpenGraphGender.Male:
                    return "male";
                case OpenGraphGender.Female:
                    return "female";
                default:
                    return string.Empty;
            }
        }
    }
}
