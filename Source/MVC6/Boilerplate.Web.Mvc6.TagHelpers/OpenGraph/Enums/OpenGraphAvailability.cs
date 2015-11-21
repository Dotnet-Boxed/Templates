namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// The stock availability of the item.
    /// </summary>
    public enum OpenGraphAvailability
    {
        /// <summary>
        /// The availability of the item is in stock.
        /// </summary>
        InStock,

        /// <summary>
        /// The availability of the item is out of stock
        /// </summary>
        OutOfStock,

        /// <summary>
        /// The availability of the item is pending or unknown.
        /// </summary>
        Pending
    }
}
