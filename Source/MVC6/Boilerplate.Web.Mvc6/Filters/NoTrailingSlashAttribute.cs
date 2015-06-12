namespace Boilerplate.Web.Mvc.Filters
{
    using System;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// Requires that a HTTP request does not contain a trailing slash. If it does, return a 404 Not Found. This is 
    /// useful if you are dynamically generating something which acts like it's a file on the web server. 
    /// E.g. /Robots.txt/ should not have a trailing slash and should be /Robots.txt. Note, that we also don't care if 
    /// it is upper-case or lower-case in this instance.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NoTrailingSlashAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Determines whether a request contains a trailing slash and, if it does, calls the 
        /// <see cref="HandleTrailingSlashRequest"/> method.
        /// </summary>
        /// <param name="context">An object that encapsulates information that is required in order to use the 
        /// <see cref="RequireHttpsAttribute"/> attribute.</param>
        public override void OnAuthorization(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            string url = context.HttpContext.Request.Path.ToString();
            if (url[url.Length - 1] == '/')
            {
                this.HandleTrailingSlashRequest(context);
            }
        }

        /// <summary>
        /// Handles HTTP requests that have a trailing slash but are not meant to.
        /// </summary>
        /// <param name="filterContext">An object that encapsulates information that is required in order to use the 
        /// <see cref="RequireHttpsAttribute"/> attribute.</param>
        protected virtual void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}