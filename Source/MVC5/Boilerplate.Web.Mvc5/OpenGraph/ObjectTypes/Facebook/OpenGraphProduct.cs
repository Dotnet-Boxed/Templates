namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a product. This includes both virtual and physical products, but it typically represents items that are available in 
    /// an online store. This object type is not part of the Open Graph standard but is used by Facebook.
    /// </summary>
    public class OpenGraphProduct : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProduct" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        public OpenGraphProduct(string title, OpenGraphImage image)
            : base(title, image)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProduct" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphProduct(string title, OpenGraphImage image, string url)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the target age group of the product, one of 'kids' or 'adult'.
        /// </summary>
        public OpenGraphAgeGroup? AgeGroup { get; set; }

        /// <summary>
        /// Gets or sets the availability of the product, one of 'instock', 'oos', or 'pending'.
        /// </summary>
        public OpenGraphAvailability? Availability { get; set; }

        /// <summary>
        /// Gets or sets the brand of the product or its original manufacturer.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the category for the product.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the colour of the product.
        /// </summary>
        public string Colour { get; set; }

        /// <summary>
        /// Gets or sets the condition of the item, one of 'new', 'refurbished', or 'used'.
        /// </summary>
        public OpenGraphCondition? Condition { get; set; }

        /// <summary>
        /// Gets or sets an International Article Number, or European Article Number (EAN), for the product.
        /// </summary>
        public string EAN { get; set; }

        /// <summary>
        /// Gets or sets a time representing when the product expired (or will expire).
        /// </summary>
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the associated story has a share button on it.
        /// </summary>
        public bool? IsShareable { get; set; }

        /// <summary>
        /// Gets or sets a International Standard Book Number (ISBN) for the product, intended for when it is a book.
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the manufacturers part number for the product.
        /// </summary>
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets a description of the material used to make the product.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "product: http://ogp.me/ns/product#"; } }

        /// <summary>
        /// Gets or sets the original prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> OriginalPrices { get; set; }

        /// <summary>
        /// Gets or sets a description of the pattern used on the product.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets a title to be used to describe multiple items of this product.
        /// </summary>
        public string PluralTitle { get; set; }

        /// <summary>
        /// Gets or sets the pre-tax prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> PretaxPrices { get; set; }

        /// <summary>
        /// Gets or sets the prices of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> Prices { get; set; }

        /// <summary>
        /// Gets or sets a URL link to find out more about the product
        /// </summary>
        public string ProductLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of times a person can purchase the product.
        /// </summary>
        public int PurchaseLimit { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the retailer of the product. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public string RetailerUrl { get; set; }

        /// <summary>
        /// Gets or sets the retailer's category for the product.
        /// </summary>
        public string RetailerCategory { get; set; }

        /// <summary>
        /// Gets or sets the retailer's part number for the product.
        /// </summary>
        public string RetailerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the retailer of the product.
        /// </summary>
        public string RetailerTitle { get; set; }

        /// <summary>
        /// Gets or sets the sale price of the product.
        /// </summary>
        public OpenGraphCurrency SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the date range for which the sale price is valid.
        /// </summary>
        public OpenGraphDateTimeRange SalePriceDates { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost of the product.
        /// </summary>
        public IEnumerable<OpenGraphCurrency> ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the shipping weight of the product.
        /// </summary>
        public OpenGraphQuantity ShippingWeight { get; set; }

        /// <summary>
        /// Gets or sets a size describing the product(such as 'S', 'M', 'L').
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the target gender for the product.
        /// </summary>
        public OpenGraphTargetGender? TargetGender { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Product; } }

        /// <summary>
        /// Gets or sets a Universal Product Code(UPC) for the product.
        /// </summary>
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the weight of the product.
        /// </summary>
        public OpenGraphQuantity Weight { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <see cref="stringBuilder" /> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            if (this.AgeGroup.HasValue)
            {
                stringBuilder.AppendMeta("product:age_group", this.AgeGroup.Value.ToLowercaseString());
            }

            if (this.Availability.HasValue)
            {
                stringBuilder.AppendMeta("product:availability", this.Availability.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaIfNotNull("product:brand", this.Brand);
            stringBuilder.AppendMetaIfNotNull("product:category", this.Category);
            stringBuilder.AppendMetaIfNotNull("product:color", this.Colour);

            if (this.Condition.HasValue)
            {
                stringBuilder.AppendMeta("product:condition", this.Condition.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaIfNotNull("product:ean", this.EAN);
            stringBuilder.AppendMetaIfNotNull("product:expiration_time", this.ExpirationTime);
            stringBuilder.AppendMetaIfNotNull("product:is_product_shareable", this.IsShareable);
            stringBuilder.AppendMetaIfNotNull("product:isbn", this.ISBN);
            stringBuilder.AppendMetaIfNotNull("product:material", this.Material);
            stringBuilder.AppendMetaIfNotNull("product:mfr_part_no", this.ManufacturerPartNumber);

            if (this.OriginalPrices != null)
            {
                foreach (OpenGraphCurrency originalPrice in this.OriginalPrices)
                {
                    stringBuilder.AppendMeta("product:original_price:amount", originalPrice.Amount);
                    stringBuilder.AppendMeta("product:original_price:currency", originalPrice.Currency);
                }
            }

            stringBuilder.AppendMetaIfNotNull("product:pattern", this.Pattern);
            stringBuilder.AppendMetaIfNotNull("product:plural_title", this.PluralTitle);

            if (this.PretaxPrices != null)
            {
                foreach (OpenGraphCurrency pretaxPrice in this.PretaxPrices)
                {
                    stringBuilder.AppendMeta("product:pretax_price:amount", pretaxPrice.Amount);
                    stringBuilder.AppendMeta("product:pretax_price:currency", pretaxPrice.Currency);
                }
            }

            if (this.PretaxPrices != null)
            {
                foreach (OpenGraphCurrency price in this.Prices)
                {
                    stringBuilder.AppendMeta("product:price:amount", price.Amount);
                    stringBuilder.AppendMeta("product:price:currency", price.Currency);
                }
            }

            stringBuilder.AppendMetaIfNotNull("product:product_link", this.ProductLinkUrl);
            stringBuilder.AppendMetaIfNotNull("product:purchase_limit", this.PurchaseLimit);
            stringBuilder.AppendMetaIfNotNull("product:retailer", this.RetailerUrl);
            stringBuilder.AppendMetaIfNotNull("product:retailer_category", this.RetailerCategory);
            stringBuilder.AppendMetaIfNotNull("product:retailer_part_no", this.RetailerPartNumber);
            stringBuilder.AppendMetaIfNotNull("product:retailer_title", this.RetailerTitle);

            if (this.SalePrice != null)
            {
                stringBuilder.AppendMeta("product:sale_price:amount", this.SalePrice.Amount);
                stringBuilder.AppendMeta("product:sale_price:currency", this.SalePrice.Currency);
            }

            if (this.SalePriceDates != null)
            {
                stringBuilder.AppendMeta("product:sale_price_dates:start", this.SalePriceDates.Start);
                stringBuilder.AppendMeta("product:sale_price_dates:end", this.SalePriceDates.End);
            }

            if (this.ShippingCost != null)
            {
                foreach (OpenGraphCurrency shippingCost in this.ShippingCost)
                {
                    stringBuilder.AppendMeta("product:shipping_cost:amount", shippingCost.Amount);
                    stringBuilder.AppendMeta("product:shipping_cost:currency", shippingCost.Currency);
                }
            }

            if (this.ShippingWeight != null)
            {
                stringBuilder.AppendMeta("product:shipping_weight:value", this.ShippingWeight.Value);
                stringBuilder.AppendMeta("product:shipping_weight:units", this.ShippingWeight.Units);
            }

            stringBuilder.AppendMetaIfNotNull("product:size", this.Size);

            if (this.TargetGender.HasValue)
            {
                stringBuilder.AppendMeta("product:target_gender", this.TargetGender.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaIfNotNull("product:upc", this.UPC);

            if (this.Weight != null)
            {
                stringBuilder.AppendMeta("product:weight:value", this.Weight.Value);
                stringBuilder.AppendMeta("product:weight:units", this.Weight.Units);
            }
        }

        #endregion
    }
}
