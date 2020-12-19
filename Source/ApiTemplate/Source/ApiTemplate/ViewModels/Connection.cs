namespace ApiTemplate.ViewModels
{
    using System.Collections.Generic;

#if Swagger
    /// <summary>
    /// A paged collection of items.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
#endif
    public class Connection<T>
    {
        public Connection() => this.Items = new List<T>();

#if Swagger
        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        /// <example>100</example>
#endif
        public int TotalCount { get; set; }

#if Swagger
        /// <summary>
        /// Gets or sets the page information.
        /// </summary>
#endif
        public PageInfo PageInfo { get; set; } = default!;

#if Swagger
        /// <summary>
        /// Gets the items.
        /// </summary>
#endif
        public List<T> Items { get; }
    }
}
