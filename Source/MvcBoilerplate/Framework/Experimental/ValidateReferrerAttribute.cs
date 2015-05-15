namespace MvcBoilerplate.Framework
{
    // EXPERIMENTAL FEATURE
    using System;
    using System.Configuration;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Represents an attribute that is used to prevent forgery of a request or Cross-Site Request Forgery (CSRF).
    /// Checks that POST requests are sent from the this site by checking that the referrer contains this sites host name.
    /// To use this attribute, you need to add an app setting in the Web.config file named 'Host', which contains this sites host name 
    /// e.g. <![CDATA[<add key="Host" value="localhost" />]]>.
    /// Note that many clients do not send the referrer with their request, so in this case we do nothing.
    /// This attribute should be used as part of a Defence-in-Depth (DID) strategy, alongside the ValidateAntiForgeryTokenAttribute, so an
    /// attacker would need to defeat multiple, independent, defenses to execute a successful attack.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ValidateReferrerAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <exception cref="System.ArgumentNullException">filterContext is null.</exception>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if ((filterContext.HttpContext.Request.UrlReferrer != null) &&
                string.Equals(filterContext.HttpContext.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(filterContext.HttpContext.Request.UrlReferrer.Host, ConfigurationManager.AppSettings["Host"], StringComparison.OrdinalIgnoreCase))
            {
                this.HandleExternalPostRequest(filterContext);
            }
        }

        /// <summary>
        /// Handles post requests that are made from an external source. 
        /// By default a 403 Forbidden response is returned.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <exception cref="System.Web.HttpException">Request not allowed.</exception>
        protected virtual void HandleExternalPostRequest(AuthorizationContext filterContext)
        {
            throw new HttpException((int)HttpStatusCode.Forbidden, "Request not allowed.");
        }
    }
}