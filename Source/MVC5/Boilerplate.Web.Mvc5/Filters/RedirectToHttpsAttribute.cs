namespace Boilerplate.Web.Mvc.Filters
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Represents an attribute that forces an unsecured HTTP request to be re-sent over HTTPS.
    /// <see cref="System.Web.Mvc.RequireHttpsAttribute"/> performs a 302 Temporary redirect from a HTTP URL to a HTTPS URL.
    /// This filter gives you the option to perform a 301 Permanent redirect or a 302 temporary redirect.
    /// You should perform a 301 permanent redirect if the page can only ever be accessed by HTTPS and a 302 temporary redirect if
    /// the page can be accessed over HTTP or HTTPS.
    /// <see cref="System.Web.Mvc.RequireHttpsAttribute"/> also throws an <see cref="InvalidOperationException"/> if request is made
    /// except GET, which returns a 500 Internal Server Error to the client. This filter, returns a 405 Method Not Allowed instead,
    /// which is much more suitable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class RedirectToHttpsAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly bool permanent;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedirectToHttpsAttribute"/> class.
        /// </summary>
        /// <param name="permanent">if set to <c>true</c> the redirection should be permanent; otherwise, <c>false</c>.</param>
        public RedirectToHttpsAttribute(bool permanent)
        {
            this.permanent = permanent;
        }

        /// <summary>
        /// Gets a value indicating whether the redirection should be permanent.
        /// </summary>
        /// <value>
        /// <c>true</c> if the redirection should be permanent; otherwise, <c>false</c>.
        /// </value>
        public bool Permanent
        {
            get { return this.permanent; }
        }

        /// <summary>
        /// Determines whether a request is secured (HTTPS) and, if it is not, calls the <see cref="HandleNonHttpsRequest"/> method.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the <see cref="System.Web.Mvc.RequireHttpsAttribute"/> attribute.</param>
        /// <exception cref="System.ArgumentNullException">The filterContext parameter is null.</exception>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (!filterContext.HttpContext.Request.IsSecureConnection)
            {
                this.HandleNonHttpsRequest(filterContext);
            }
        }

        /// <summary>
        /// Handles unsecured HTTP requests that are sent to the action method.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the <see cref="System.Web.Mvc.RequireHttpsAttribute"/> attribute.</param>
        /// <exception cref="System.InvalidOperationException">The HTTP request contains an invalid transfer method override. All GET requests are considered invalid.</exception>
        protected virtual void HandleNonHttpsRequest(AuthorizationContext filterContext)
        {
            // Only redirect for GET requests, otherwise the browser might not propagate the verb and request body correctly.
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, WebRequestMethods.Http.Get, StringComparison.OrdinalIgnoreCase))
            {
                // The RequireHttpsAttribute throws an InvalidOperationException. Some bots and spiders make HEAD requests (to reduce bandwidth) 
                // and we don’t want them to see a 500-Internal Server Error. A 405 Method Not Allowed would be more appropriate.
                throw new HttpException((int)HttpStatusCode.MethodNotAllowed, "Method Not Allowed");
            }

            string url = "https://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url, this.permanent);
        }
    }
}