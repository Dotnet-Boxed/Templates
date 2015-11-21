namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A split value in a fitness event.
    /// </summary>
    public class OpenGraphSplit
    {
        private readonly bool isMiles;
        private readonly IEnumerable<OpenGraphQuantity> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphSplit"/> class.
        /// </summary>
        /// <param name="isMiles">if set to <c>true</c> distance is measured in miles, otherwise kilometres.</param>
        /// <param name="values">The values.</param>
        /// <exception cref="System.ArgumentNullException">values</exception>
        public OpenGraphSplit(bool isMiles, IEnumerable<OpenGraphQuantity> values)
        {
            if (values == null) { throw new ArgumentNullException(nameof(values)); }

            this.isMiles = isMiles;
            this.values = values;
        }

        /// <summary>
        /// Gets a value indicating whether distance is measured in miles.
        /// </summary>
        public bool IsMiles { get { return this.isMiles; } }

        /// <summary>
        /// Gets a value indicating whether distance is measured in kilometres.
        /// </summary>
        public bool IsKilometers { get { return !this.isMiles; } }

        /// <summary>
        /// Gets the split values.
        /// </summary>
        public IEnumerable<OpenGraphQuantity> Values { get { return this.values; } }
    }
}
