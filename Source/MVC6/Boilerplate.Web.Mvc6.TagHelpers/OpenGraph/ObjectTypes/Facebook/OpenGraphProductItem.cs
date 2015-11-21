namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.AspNet.Razor.TagHelpers;

    /// <summary>
    /// This object type represents a product item. This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/product.item/
    /// </summary>
    [HtmlTargetElement(
        "open-graph-product-item", 
        Attributes = TitleAttributeName + "," + MainImageAttributeName + "," + AvailabilityAttributeName + "," + ConditionAttributeName + "," + PricesAttributeName + "," + RetailerItemIdAttributeName, 
        TagStructure = TagStructure.WithoutEndTag)]
    public class OpenGraphProductItem : OpenGraphMetadata
    {
        #region Constants

        private const string AgeGroupAttributeName = "age-group";
        private const string AvailabilityAttributeName = "availability";
        private const string BrandAttributeName = "brand";
        private const string CategoryAttributeName = "category";
        private const string ColourAttributeName = "colour";
        private const string ConditionAttributeName = "condition";
        private const string ExpirationTimeAttributeName = "expiration-time";
        private const string GroupUrlAttributeName = "group-url";
        private const string GTINAttributeName = "gtin";
        private const string ManufacturerPartNumberAttributeName = "manufacturer-part-number";
        private const string MaterialAttributeName = "material";
        private const string PatternAttributeName = "pattern";
        private const string PricesAttributeName = "prices";
        private const string RetailerCategoryAttributeName = "retailer-category";
        private const string RetailerGroupIdAttributeName = "retailer-group-id";
        private const string RetailerItemIdAttributeName = "retailer-item-id";
        private const string SalePriceAttributeName = "sale-price";
        private const string SalePriceDatesAttributeName = "sale-price-dates";
        private const string ShippingCostAttributeName = "shipping-cost";
        private const string ShippingWeightAttributeName = "shipping-weight";
        private const string SizeAttributeName = "size";
        private const string TargetGenderAttributeName = "target-gender";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the target age group of the item, one of 'kids' or 'adult'.
        /// </summary>
        [HtmlAttributeName(AgeGroupAttributeName)]
        public OpenGraphAgeGroup? AgeGroup { get; set; }

        /// <summary>
        /// Gets or sets the availability of the item, one of 'instock', 'oos', or 'pending'.
        /// </summary>
        [HtmlAttributeName(AvailabilityAttributeName)]
        public OpenGraphAvailability Availability { get; set; }

        /// <summary>
        /// Gets or sets the brand of the item or its original manufacturer.
        /// </summary>
        [HtmlAttributeName(BrandAttributeName)]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the category for the item.
        /// </summary>
        [HtmlAttributeName(CategoryAttributeName)]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the colour of the item.
        /// </summary>
        [HtmlAttributeName(ColourAttributeName)]
        public string Colour { get; set; }

        /// <summary>
        /// Gets or sets the condition of the item, one of 'new', 'refurbished', or 'used'.
        /// </summary>
        [HtmlAttributeName(ConditionAttributeName)]
        public OpenGraphCondition Condition { get; set; }

        /// <summary>
        /// Gets or sets a time representing when the item expired (or will expire).
        /// </summary>
        [HtmlAttributeName(ExpirationTimeAttributeName)]
        public DateTime? ExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets the URL to the page about the product group. This URL must contain profile meta tags <see cref="OpenGraphProductGroup"/>.
        /// </summary>
        [HtmlAttributeName(GroupUrlAttributeName)]
        public string GroupUrl { get; set; }

        /// <summary>
        /// Gets or sets the Global Trade Item Number (GTIN), which encompasses UPC, EAN, JAN, and ISBN.
        /// </summary>
        [HtmlAttributeName(GTINAttributeName)]
        public string GTIN { get; set; }

        /// <summary>
        /// Gets or sets the manufacturers part number for the item.
        /// </summary>
        [HtmlAttributeName(ManufacturerPartNumberAttributeName)]
        public string ManufacturerPartNumber { get; set; }

        /// <summary>
        /// Gets or sets a description of the material used to make the item.
        /// </summary>
        [HtmlAttributeName(MaterialAttributeName)]
        public string Material { get; set; }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace { get { return "product: http://ogp.me/ns/product#"; } }

        /// <summary>
        /// Gets or sets a description of the pattern used on the item.
        /// </summary>
        [HtmlAttributeName(PatternAttributeName)]
        public string Pattern { get; set; }

        /// <summary>
        /// Gets or sets the prices of the item.
        /// </summary>
        [HtmlAttributeName(PricesAttributeName)]
        public IEnumerable<OpenGraphCurrency> Prices { get; set; }

        /// <summary>
        /// Gets or sets the retailer's category for the item.
        /// </summary>
        [HtmlAttributeName(RetailerCategoryAttributeName)]
        public string RetailerCategory { get; set; }

        /// <summary>
        /// Gets or sets the retailer product group ID for this item.
        /// </summary>
        [HtmlAttributeName(RetailerGroupIdAttributeName)]
        public string RetailerGroupId { get; set; }

        /// <summary>
        /// Gets or sets the retailer's ID for the item.
        /// </summary>
        [HtmlAttributeName(RetailerItemIdAttributeName)]
        public string RetailerItemId { get; set; }

        /// <summary>
        /// Gets or sets the sale price of the item.
        /// </summary>
        [HtmlAttributeName(SalePriceAttributeName)]
        public OpenGraphCurrency SalePrice { get; set; }

        /// <summary>
        /// Gets or sets the date range for which the sale price is valid.
        /// </summary>
        [HtmlAttributeName(SalePriceDatesAttributeName)]
        public OpenGraphDateTimeRange SalePriceDates { get; set; }

        /// <summary>
        /// Gets or sets the shipping cost of the item.
        /// </summary>
        [HtmlAttributeName(ShippingCostAttributeName)]
        public IEnumerable<OpenGraphCurrency> ShippingCost { get; set; }

        /// <summary>
        /// Gets or sets the shipping weight of the item.
        /// </summary>
        [HtmlAttributeName(ShippingWeightAttributeName)]
        public OpenGraphQuantity ShippingWeight { get; set; }

        /// <summary>
        /// Gets or sets a size describing the item (such as 'S', 'M', 'L').
        /// </summary>
        [HtmlAttributeName(SizeAttributeName)]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the target gender for the item.
        /// </summary>
        [HtmlAttributeName(TargetGenderAttributeName)]
        public OpenGraphTargetGender? TargetGender { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type { get { return OpenGraphType.ProductItem; } }

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

            stringBuilder.AppendMetaPropertyContent("product:availability", this.Availability.ToLowercaseString());
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:brand", this.Brand);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:category", this.Category);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:color", this.Colour);
            stringBuilder.AppendMetaPropertyContent("product:condition", this.Condition.ToLowercaseString());
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:expiration_time", this.ExpirationTime);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:group_ref", this.GroupUrl);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:gtin", this.GTIN);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:mfr_part_no", this.ManufacturerPartNumber);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:material", this.Material);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:pattern", this.Pattern);

            foreach (OpenGraphCurrency price in this.Prices)
            {
                stringBuilder.AppendMetaPropertyContent("product:price:amount", price.Amount);
                stringBuilder.AppendMetaPropertyContent("product:price:currency", price.Currency);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_category", this.RetailerCategory);
            stringBuilder.AppendMetaPropertyContentIfNotNull("product:retailer_group_id", this.RetailerGroupId);
            stringBuilder.AppendMetaPropertyContent("product:retailer_item_id", this.RetailerItemId);

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
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Checks that this instance is valid and throws exceptions if not valid.
        /// </summary>
        protected override void Validate()
        {
            base.Validate();

            if (this.Prices == null) { throw new ArgumentNullException(nameof(this.Prices)); }
            if (this.RetailerItemId == null) { throw new ArgumentNullException(nameof(this.RetailerItemId)); }
        }

        #endregion
    }
}