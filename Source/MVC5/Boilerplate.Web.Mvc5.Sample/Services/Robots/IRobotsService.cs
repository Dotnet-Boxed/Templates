namespace MvcBoilerplate.Services
{
    public interface IRobotsService
    {
        /// <summary>
        /// Gets the robots text for the current site. This tells search engines (or robots) how to index your site.
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths. See 
        /// http://en.wikipedia.org/wiki/Robots_exclusion_standard
        /// Note: Disallowing crawling of JavaScript or CSS files in your sites robots.txt directly harms how well 
        /// Google's algorithms render and index your content and can result in suboptimal rankings.
        /// </summary>
        /// <returns>The robots text for the current site.</returns>
        string GetRobotsText();
    }
}
