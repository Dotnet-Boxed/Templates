namespace MvcBoilerplate.Framework.OpenGraph
{
    /// <summary>
    /// An image which should represent your object within the graph.
    /// </summary>
    public class Image : SizedMedia
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="imageUrl">The image URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if imageUrl is <c>null</c>.</exception>
        public Image(string imageUrl)
            : base(imageUrl)
        {
        }
    }
}
