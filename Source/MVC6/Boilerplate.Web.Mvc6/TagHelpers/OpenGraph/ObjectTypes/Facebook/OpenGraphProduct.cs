namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.Runtime.TagHelpers;

    /// <summary>
    /// This object type represents a product. This includes both virtual and physical products, but it typically represents items that are available in 
    /// an online store. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product/
    /// </summary>
    [TargetElement(Attributes = nameof(Title) + "," + nameof(MainImage), TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphProduct : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProduct" /> class.
        /// </summary>
        public OpenGraphProduct() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphProduct" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="mainImage">The main image which should represent your object within the graph. This is a required property.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph. Leave as <c>null</c> to get the URL of the current page.</param>
        public OpenGraphProduct(string title, OpenGraphImage mainImage, string url = null)
            : base(title, mainImage, url)
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
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            if (this.AgeGroup.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:age_group", this.AgeGroup.Value.ToLowercaseString());
            }

            if (this.Availability.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:availability", this.Availability.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:brand", this.Brand);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:category", this.Category);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:color", this.Colour);

            if (this.Condition.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:condition", this.Condition.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:ean", this.EAN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:expiration_time", this.ExpirationTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:is_product_shareable", this.IsShareable);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:isbn", this.ISBN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:material", this.Material);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:mfr_part_no", this.ManufacturerPartNumber);

            if (this.OriginalPrices != null)
            {
                foreach (OpenGraphCurrency originalPrice in this.OriginalPrices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:original_price:amount", originalPrice.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:original_price:currency", originalPrice.Currency);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:pattern", this.Pattern);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:plural_title", this.PluralTitle);

            if (this.PretaxPrices != null)
            {
                foreach (OpenGraphCurrency pretaxPrice in this.PretaxPrices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:pretax_price:amount", pretaxPrice.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:pretax_price:currency", pretaxPrice.Currency);
                }
            }

            if (this.Prices != null)
            {
                foreach (OpenGraphCurrency price in this.Prices)
                {
                    stringBuilder.AppendMetaPropertyContent("product:price:amount", price.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:price:currency", price.Currency);
                }
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:product_link", this.ProductLinkUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:purchase_limit", this.PurchaseLimit);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer", this.RetailerUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_category", this.RetailerCategory);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_part_no", this.RetailerPartNumber);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_title", this.RetailerTitle);

            if (this.SalePrice != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:sale_price:amount", this.SalePrice.Amount);
                stringBuilder.AppendMetaPropertyContent("product:sale_price:currency", this.SalePrice.Currency);
            }

            if (this.SalePriceDates != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:sale_price_dates:start", this.SalePriceDates.Start);
                stringBuilder.AppendMetaPropertyContent("product:sale_price_dates:end", this.SalePriceDates.End);
            }

            if (this.ShippingCost != null)
            {
                foreach (OpenGraphCurrency shippingCost in this.ShippingCost)
                {
                    stringBuilder.AppendMetaPropertyContent("product:shipping_cost:amount", shippingCost.Amount);
                    stringBuilder.AppendMetaPropertyContent("product:shipping_cost:currency", shippingCost.Currency);
                }
            }

            if (this.ShippingWeight != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:shipping_weight:value", this.ShippingWeight.Value);
                stringBuilder.AppendMetaPropertyContent("product:shipping_weight:units", this.ShippingWeight.Units);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:size", this.Size);

            if (this.TargetGender.HasValue)
            {
                stringBuilder.AppendMetaPropertyContent("product:target_gender", this.TargetGender.Value.ToLowercaseString());
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:upc", this.UPC);

            if (this.Weight != null)
            {
                stringBuilder.AppendMetaPropertyContent("product:weight:value", this.Weight.Value);
                stringBuilder.AppendMetaPropertyContent("product:weight:units", this.Weight.Units);
            }
        }

        #endregion
    }
}
