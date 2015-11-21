#if NET451
namespace Boilerplate.Web.Mvc
{
    using System.ServiceModel.Syndication;
    using System.Text;
    using System.Xml;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Hosting;

    /// <summary>
    /// Represents a class that is used to render an Atom 1.0 feed by using an <see cref="SyndicationFeed"/> instance 
    /// representing the feed.
    /// </summary>
    public sealed class AtomActionResult : ActionResult
    {
        private readonly SyndicationFeed syndicationFeed;

        /// <summary>
        /// Initializes a new instance of the <see cref="AtomActionResult"/> class.
        /// </summary>
        /// <param name="syndicationFeed">The Atom 1.0 <see cref="SyndicationFeed" />.</param>
        public AtomActionResult(SyndicationFeed syndicationFeed)
        {
            this.syndicationFeed = syndicationFeed;
        }

        /// <summary>
        /// Executes the call to the ActionResult method and returns the created feed to the output response.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the 
        /// controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ActionContext context)
        {
            context.HttpContext.Response.ContentType = ContentType.Atom;
            Atom10FeedFormatter feedFormatter = new Atom10FeedFormatter(this.syndicationFeed);
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Encoding = Encoding.UTF8;

            if (Context.Environment.IsDevelopment())
            {
                // Indent the XML for easier viewing but only in Debug mode. In Release mode, everything is output on 
                // one line for best performance.
                xmlWriterSettings.Indent = true;
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(context.HttpContext.Response.Body, xmlWriterSettings))
            {
                feedFormatter.WriteTo(xmlWriter);
            }
        }
    }
}
#endif