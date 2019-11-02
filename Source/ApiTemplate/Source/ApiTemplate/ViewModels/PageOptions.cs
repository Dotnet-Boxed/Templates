namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

#if Swagger
    /// <summary>
    /// The options used to request a page.
    /// </summary>
#endif
    public class PageOptions
    {
#if Swagger
        /// <summary>
        /// Gets or sets the number of items to retrieve from the beginning.
        /// </summary>
        /// <example>10</example>
#endif
        [Range(1, 20)]
        public int? First { get; set; }

#if Swagger
        /// <summary>
        /// Gets or sets the number of items to retrieve from the end.
        /// </summary>
        /// <example></example>
#endif
        [Range(1, 20)]
        public int? Last { get; set; }

#if Swagger
        /// <summary>
        /// Gets or sets the cursor of the item after which items are requested.
        /// </summary>
        /// <example></example>
#endif
        public string After { get; set; }

#if Swagger
        /// <summary>
        /// Gets or sets the cursor of the item before which items are requested
        /// </summary>
        /// <example></example>
#endif
        public string Before { get; set; }
    }
}
