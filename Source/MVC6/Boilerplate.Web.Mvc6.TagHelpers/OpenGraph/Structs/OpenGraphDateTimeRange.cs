namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// A date and time range.
    /// </summary>
    public class OpenGraphDateTimeRange
    {
        private readonly DateTime end;
        private readonly DateTime start;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphDateTimeRange"/> class.
        /// </summary>
        /// <param name="start">The start date and time.</param>
        /// <param name="end">The end date and time.</param>
        public OpenGraphDateTimeRange(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// Gets the end date and time.
        /// </summary>
        public DateTime End { get { return this.end; } }

        /// <summary>
        /// Gets the start date and time.
        /// </summary>
        public DateTime Start { get { return this.start; } }
    }
}
