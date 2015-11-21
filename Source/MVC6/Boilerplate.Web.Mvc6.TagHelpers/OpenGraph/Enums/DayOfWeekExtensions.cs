namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// <see cref="DayOfWeek"/> extension methods.
    /// </summary>
    internal static class DayOfWeekExtensions
    {
        /// <summary>
        /// Returns the lowercase <see cref="string" /> representation of the <see cref="DayOfWeek" />.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>
        /// The lowercase <see cref="string" /> representation of the <see cref="DayOfWeek" />.
        /// </returns>
        public static string ToLowercaseString(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "friday";
                case DayOfWeek.Monday:
                    return "monday";
                case DayOfWeek.Saturday:
                    return "saturday";
                case DayOfWeek.Sunday:
                    return "sunday";
                case DayOfWeek.Thursday:
                    return "thursday";
                case DayOfWeek.Tuesday:
                    return "tuesday";
                case DayOfWeek.Wednesday:
                    return "wednesday";
                default:
                    return string.Empty;
            }
        }
    }
}
