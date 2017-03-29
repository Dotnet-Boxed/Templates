namespace MvcBoilerplate.Services
{
    using System.Text;
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using MvcBoilerplate.Constants;

    public sealed class RobotsService : IRobotsService
    {
        private readonly IUrlHelper urlHelper;

        public RobotsService(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        /// <summary>
        /// Gets the robots text for the current site. This tells search engines (or robots) how to index your site.
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths. See
        /// http://rehansaeed.com/dynamically-generating-robots-txt-using-asp-net-mvc/
        /// Note: Disallowing crawling of JavaScript or CSS files in your sites robots.txt directly harms how well
        /// Google's algorithms render and index your content and can result in suboptimal rankings.
        /// </summary>
        /// <returns>The robots text for the current site.</returns>
        public string GetRobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Allow all robots.
            stringBuilder.AppendLine("user-agent: *");

            // Tell all robots not to index any directories.
            // stringBuilder.AppendLine("disallow: /");

            // Tell all robots not to index everything under the following directory.
            // stringBuilder.AppendLine("disallow: /SomeRelativePath");

            // Tell all robots to to index any of the error pages.
            stringBuilder.AppendLine("disallow: /error/");

            // Tell all robots they can visit everything under the following sub-directory, even if the parent
            // directory is disallowed.
            // stringBuilder.AppendLine("allow: /SomeRelativePath/SomeSubDirectory");

            // Tell all robots the number of seconds to wait between successive requests to the same server. This can
            // be useful if your site cannot handle large loads placed on it by robots. Note that this is a
            // non-standard extension and not all robots respect it.
            // stringBuilder.AppendLine("crawl-delay: 10"); // Delay by 1- seconds.

            // SECURITY ALERT - BE CAREFUL WHAT YOU ADD HERE
            // The line below stops all robots from indexing the following secret folder. For example, this could be
            // your Elmah error logs. Very useful to any hacker. You should be securing these pages using some form of
            // authentication but hiding where these things are can help through a bit of security through obscurity.
            // stringBuilder.AppendLine("disallow: /MySecretStuff");
            // $Start-Sitemap$

            // Add a link to the sitemap. Unfortunately this must be an absolute URL.
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.urlHelper.AbsoluteRouteUrl(HomeControllerRoute.GetSitemapXml).TrimEnd('/'));
            // $End-Sitemap$

            return stringBuilder.ToString();
        }
    }
}