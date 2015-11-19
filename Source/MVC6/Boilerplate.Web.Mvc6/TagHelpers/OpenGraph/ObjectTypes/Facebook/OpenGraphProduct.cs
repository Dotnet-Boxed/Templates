namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a product. This includes both virtual and physical products, but it typically represents items that are available in 
    /// an online store. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-product", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphProduct : OpenGraphMetadata
    {
        #region Constructors

        private const string AgeGroupAttributeName = "age-group";
        private const string AvailabilityAttributeName = "availability";
        private const string BrandAttributeName = "brand";
        private const string CategoryAttributeName = "category";
        private const string ColourAttributeName = "colour";
        private const string ConditionAttributeName = "condition";
        private const string EANAttributeName = "ean";
        private const string ExpirationTimeAttributeName = "expiration-time";
        private const string IsShareableAttributeName = "shareable";
        private const string ISBNAttributeName = "isbn";
        private const string ManufacturerPartNumberAttributeName = "manufacturer-part-number";
        private const string MaterialAttributeName = "material";
        private const string OriginalPricesAttributeName = "original-prices";
        private const string PatternAttributeName = "pattern";
        private const string PluralTitleAttributeName = "plural-title";
        private const string PretaxPricesAttributeName = "pretax-prices";
        private const string PricesAttributeName = "prices";
        private const string ProductLinkUrlAttributeName = "product-link-url";
        private const string PurchaseLimitAttributeName = "purchase-limit";
        private const string RetailerUrlAttributeName = "retailer-url";
        private const string RetailerCategoryAttributeName = "retailer-category";
        private const string RetailerPartNumberAttributeName = "retailer-part-number";
        private const string RetailerTitleAttributeName = "retailer-title";
        private const string SalePriceAttributeName = "sale-price";
        private const string SalePriceDatesAttributeName = "sale-price-dates";
        private const string ShippingCostAttributeName = "shipping-cost";
        private const string ShippingWeightAttributeName = "shipping-weight";
        private const string SizeAttributeName = "size";
        private const string TargetGenderAttributeName = "target-gender";
        private const string WeightAttributeName = "weight";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the target age group of the product, one of 'kids' or 'adult'.
        /// </summary>
        [HtmlAttributeName(AgeGroupAttributeName)]
        public OpenGraphAgeGroup? AgeGroup { get; set; }

        /// <summary>
        /// Gets or sets the availability of the product, one of 'instock', 'oos', or 'pending'.
        /// </summary>
        [HtmlAttributeName(AvailabilityAttributeName)]
        public OpenGraphAvailability? Availability { get; set; }

        /// <summary>
        /// Gets or sets the brand of the product or its original manufacturer.
        /// </summary>
        [HtmlAttributeName(BrandAttributeName)]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the category for the product.
        /// </summary>
        [HtmlAttributeName(CategoryAttributeName)]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the colour of the product.
        /// </summary>
        [HtmlAttributeName(ColourAttributeName)]
        public string Colour { get; set; }

        /// <summary>
        /// Gets or sets the condition of the item, one of 'new', 'refurbished', or 'used'.
        /// </summary>
        [HtmlAttributeName(ConditionAttributeName)]
        public OpenGraphCondition? Condition { get; set; }

        /// <summary>
        /// Gets or sets an International Article Number, or European Article Number (EAN), for the product.
        /// </summary>
        [HtmlAttributeName(EANAttributeName)]
        public string EAN { get; set; }

        /// <summary>
        /// Gets or sets a time representing when the product expired (or will expire).
        /// </summary>
        [HtmlAttributeName(ExpirationTimeAttributeName)]
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the associated story has a share button on it.
        /// </summary>
        [HtmlAttributeName(IsShareableAttributeName)]
        public bool? IsShareable { get; set; }

        /// <summary>
        /// Gets or sets a International Standard Book Number (ISBN) for the product, intended for when it is a book.
        /// </summary>
        [HtmlAttributeName(ISBNAttributeName)]
        public string ISBN { get; set; }

        /// <summary>
        /// Gets or sets the manufacturers part number for the product.
        /// </summary>
        [HtmlAttributeName(ManufacturerPartNumberAttributeName)]
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets a description of the material used to make the product.
        /// </summary>
        [HtmlAttributeName(MaterialAttributeName)]
        public string Material { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "product: http://ogp.me/ns/product#"; } }

        /// <summary>
        /// Gets or sets the original prices of the product.
        /// </summary>
        [HtmlAttributeName(OriginalPricesAttributeName)]
        public IEnumerable<OpenGraphCurrency> OriginalPrices { get; set; }

        /// <summary>
        /// Gets or sets a description of the pattern used on the product.
        /// </summary>
        [HtmlAttributeName(PatternAttributeName)]
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets a title to be used to describe multiple items of this product.
        /// </summary>
        [HtmlAttributeName(PluralTitleAttributeName)]
        public string PluralTitle { get; set; }

        /// <summary>
        /// Gets or sets the pre-tax prices of the product.
        /// </summary>
        [HtmlAttributeName(PretaxPricesAttributeName)]
        public IEnumerable<OpenGraphCurrency> PretaxPrices { get; set; }

        /// <summary>
        /// Gets or sets the prices of the product.
        /// </summary>
        [HtmlAttributeName(PricesAttributeName)]
        public IEnumerable<OpenGraphCurrency> Prices { get; set; }

        /// <summary>
        /// Gets or sets a URL link to find out more about the product
        /// </summary>
        [HtmlAttributeName(ProductLinkUrlAttributeName)]
        public string ProductLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of times a person can purchase the product.
        /// </summary>
        [HtmlAttributeName(PurchaseLimitAttributeName)]
        public int PurchaseLimit { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the retailer of the product. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        [HtmlAttributeName(RetailerUrlAttributeName)]
        public string RetailerUrl { get; set; }

        /// <summary>
        /// Gets or sets the retailer's category for the product.
        /// </summary>
        [HtmlAttributeName(RetailerCategoryAttributeName)]
        public string RetailerCategory { get; set; }

        /// <summary>
        /// Gets or sets the retailer's part number for the product.
        /// </summary>
        [HtmlAttributeName(RetailerPartNumberAttributeName)]
        public string RetailerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the retailer of the product.
        /// </summary>
        [HtmlAttributeName(RetailerTitleAttributeName)]
        public string RetailerTitle { get; set; }

        /// <summary>
        /// Gets or sets the sale price of the product.
        /// </summary>
        [HtmlAttributeName(SalePriceAttributeName)]
        public OpenGraphCurrency SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the date range for which the sale price is valid.
        /// </summary>
        [HtmlAttributeName(SalePriceDatesAttributeName)]
        public OpenGraphDateTimeRange SalePriceDates { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost of the product.
        /// </summary>
        [HtmlAttributeName(ShippingCostAttributeName)]
        public IEnumerable<OpenGraphCurrency> ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the shipping weight of the product.
        /// </summary>
        [HtmlAttributeName(ShippingWeightAttributeName)]
        public OpenGraphQuantity ShippingWeight { get; set; }

        /// <summary>
        /// Gets or sets a size describing the product(such as 'S', 'M', 'L').
        /// </summary>
        [HtmlAttributeName(SizeAttributeName)]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the target gender for the product.
        /// </summary>
        [HtmlAttributeName(TargetGenderAttributeName)]
        public OpenGraphTargetGender? TargetGender { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.Product; } }

        /// <summary>
        /// Gets or sets a Universal Product Code(UPC) for the product.
        /// </summary>
        [HtmlAttributeName(TargetGenderAttributeName)]
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the weight of the product.
        /// </summary>
        [HtmlAttributeName(WeightAttributeName)]
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
