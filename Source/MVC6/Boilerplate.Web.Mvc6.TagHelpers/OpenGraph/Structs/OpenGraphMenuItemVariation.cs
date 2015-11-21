namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;

    /// <summary>
    /// Represents the name and price of a menu item variation.
    /// </summary>
    public class OpenGraphMenuItemVariation
    {
        private readonly string name;
        private readonly OpenGraphCurrency price;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMenuItemVariation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <exception cref="System.ArgumentNullException">name or price is <c>null</c>.</exception>
        public OpenGraphMenuItemVariation(string name, OpenGraphCurrency price)
        {
            if (name == null) { throw new ArgumentNullException(nameof(name)); }
            if (price == null) { throw new ArgumentNullException(nameof(price)); }

            this.name = name;
            this.price = price;
        }

        /// <summary>
        /// Gets the name of the menu item variation.
        /// </summary>
        public string Name { get { return this.name; } }

        /// <summary>
        /// Gets the price of the menu item variation.
        /// </summary>
        public OpenGraphCurrency Price { get { return this.price; } }
    }
}
