namespace MvcBoilerplate.Framework.OpenGraph
{
    /// <summary>
    /// A audio file that complements this object.
    /// </summary>
    public class Audio : Media
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        /// <param name="audioUrl">The audio URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if audioUrl is <c>null</c>.</exception>
        public Audio(string audioUrl)
            : base(audioUrl)
        {
        }
    }
}
