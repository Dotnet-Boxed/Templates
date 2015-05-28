namespace $safeprojectname$.Services
{
    public interface IOpenSearchService
    {
        /// <summary>
        /// Gets the Open Search XML for the current site. You can customize the contents of this XML here.
        /// See <see cref="http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d#commentstart"/> 
        /// and <see cref="http://www.opensearch.org"/> for more information.
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        string GetOpenSearchXml();
    }
}
