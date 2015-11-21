namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// A single data point in a fitness activity.
    /// </summary>
    public class OpenGraphFitnessActivityDataPoint
    {
        /// <summary>
        /// Gets or sets an integer representing the number of calories used during a distinct part of the course.
        /// </summary>
        public int? Calories { get; set; }

        /// <summary>
        /// Gets or sets the energy used during a distinct part of the course.
        /// </summary>
        public OpenGraphQuantity CustomUnitEnergy { get; set; }

        /// <summary>
        /// Gets or sets the distance covered during a distinct part of the course.
        /// </summary>
        public OpenGraphQuantity Distance { get; set; }

        /// <summary>
        /// Gets or sets the location of a distinct part of the course.
        /// </summary>
        public OpenGraphLocation Location { get; set; }

        /// <summary>
        /// Gets or sets the pace achieved during distinct parts of the course.
        /// </summary>
        public OpenGraphQuantity Pace { get; set; }

        /// <summary>
        /// Gets or sets the speed achieved during a distinct part of the course.
        /// </summary>
        public OpenGraphQuantity Speed { get; set; }

        /// <summary>
        /// Gets or sets the number of steps taken during a distinct part of the course.
        /// </summary>
        public int? Steps { get; set; }

        /// <summary>
        /// Gets or sets the time of a distinct parts of the course.
        /// </summary>
        public DateTime? Timestamp { get; set; }
    }
}
