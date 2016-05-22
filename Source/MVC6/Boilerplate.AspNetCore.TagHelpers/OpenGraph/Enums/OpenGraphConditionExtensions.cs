namespace Boilerplate.AspNetCore.TagHelpers.OpenGraph
{
    /// <summary>
    /// <see cref="OpenGraphCondition"/> extension methods.
    /// </summary>
    internal static class OpenGraphConditionExtensions
    {
        /// <summary>
        /// Returns the lower-case <see cref="string" /> representation of the <see cref="OpenGraphCondition" />.
        /// </summary>
        /// <param name="openGraphCondition">The open graph condition of the item.</param>
        /// <returns>
        /// The lower-case <see cref="string" /> representation of the <see cref="OpenGraphCondition" />.
        /// </returns>
        public static string ToLowercaseString(this OpenGraphCondition openGraphCondition)
        {
            switch (openGraphCondition)
            {
                case OpenGraphCondition.New:
                    return "new";
                case OpenGraphCondition.Refurbished:
                    return "refurbished";
                case OpenGraphCondition.Used:
                    return "used";
                default:
                    return string.Empty;
            }
        }
    }
}
