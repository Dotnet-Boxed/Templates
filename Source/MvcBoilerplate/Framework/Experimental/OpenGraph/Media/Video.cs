namespace MvcBoilerplate.Framework.OpenGraph
{
    /// <summary>
    /// A video file that complements this object.
    /// </summary>
    public class Video : SizedMedia
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        /// <param name="videoUrl">The video URL.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if videoUrl is <c>null</c>.</exception>
        public Video(string videoUrl)
            : base(videoUrl)
        {
        }
    }
}
