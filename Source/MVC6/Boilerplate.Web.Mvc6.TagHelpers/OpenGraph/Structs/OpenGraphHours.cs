namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// A period of time on the specified day.
    /// </summary>
    public class OpenGraphHours
    {
        private readonly DayOfWeek day;
        private readonly TimeSpan end;
        private readonly TimeSpan start;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphHours"/> class.
        /// </summary>
        /// <param name="day">The day in the week.</param>
        /// <param name="start">The start time of the day. This can be a value from 00:00 to 24:00 hours.</param>
        /// <param name="end">The end time of the day. This can be a value from 00:00 to 24:00 hours.</param>
        public OpenGraphHours(DayOfWeek day, TimeSpan start, TimeSpan end)
        {
            this.day = day;
            this.end = end;
            this.start = start;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the day in the week.
        /// </summary>
        public DayOfWeek Day { get { return this.day; } }

        /// <summary>
        /// Gets the end time of the day. This can be a value from 00:00 to 24:00 hours.
        /// </summary>
        public TimeSpan End { get { return this.end; } }

        /// <summary>
        /// Gets the start time of the day. This can be a value from 00:00 to 24:00 hours.
        /// </summary>
        public TimeSpan Start { get { return this.start; } } 

        #endregion
    }
}
