namespace ApiTemplate.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// A paged collection of items.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public class Connection<T>
    {
        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        /// <example>100</example>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the page information.
        /// </summary>
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public List<T> Items { get; set; }
    }
}
