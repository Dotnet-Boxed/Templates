namespace Boilerplate.Web.Mvc.TagHelpers.OpenGraph
{
    /// <summary>
    /// Represents the rating on some scale for an object.
    /// </summary>
    public class OpenGraphRating
    {
        private readonly int scale;
        private readonly int value;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphRating"/> class.
        /// </summary>
        /// <param name="value">The value of the rating given to the object.</param>
        /// <param name="scale">The highest value possible in the rating scale.</param>
        public OpenGraphRating(int value, int scale)
        {
            this.scale = scale;
            this.value = value;
        }

        /// <summary>
        /// Gets the highest value possible in the rating scale.
        /// </summary>
        public int Scale { get { return this.scale; } }

        /// <summary>
        /// Gets the value of the rating given to the object.
        /// </summary>
        public double Value { get { return this.value; } }
    }
}
