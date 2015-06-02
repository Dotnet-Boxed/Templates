namespace Boilerplate.Web.Mvc.Filters
{
    using System;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    /// <summary>
    /// Represents an attribute that is used to prevent forgery of a request. Same as <see cref="ValidateAntiForgeryTokenAttribute"/>, 
    /// except this instance checks the HTTP Headers instead of the form inputs for one of the tokens. The other token is expected to be
    /// in the cookie as normal. This is usefule for performing Ajax requests.
    /// See http://stackoverflow.com/questions/4074199/jquery-ajax-calls-and-the-html-antiforgerytoken.
    /// </summary>
    /// <example>
    /// On the client side, when doing an ajax request from jQuery, the anti-forgery token HTTP header can be added like so:
    /// <code>
    /// $.ajax(
    ///     "postlocation", 
    ///     {
    ///         type: "post",
    ///         contentType: "application/json",
    ///         data: {  }, // JSON data goes here
    ///         dataType: "json",
    ///         headers: {
    ///             'X-RequestVerificationToken': '@TokenHeaderValue()'
    ///         }
    ///     });
    /// </code>
    /// On the server side, you simply need to add the <see cref="ValidateHeaderAntiForgeryTokenAttribute"/> attribute to the controller
    /// or action the same way you would use <see cref="ValidateAntiForgeryTokenAttribute"/>.
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateHeaderAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// The name of the request verification token HTTP header i.e. X-RequestVerificationToken.
        /// </summary>
        public const string RequestVerificationTokenHttpHeaderName = "X-RequestVerificationToken";

        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        /// <exception cref="System.ArgumentNullException">The filterContext parameter is null.</exception>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) { throw new ArgumentNullException("filterContext"); }

            HttpRequestBase request = filterContext.HttpContext.Request;
            string headerTokenValue = request.Headers[RequestVerificationTokenHttpHeaderName];

            // Ajax POSTs using jquery have a header set that defines the token.
            // However using unobtrusive ajax the token is still submitted normally in the form.
            // if the header is present then use it, else fall back to processing the form like normal.
            if (headerTokenValue != null)
            {
                HttpCookie antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];
                string cookieValue = antiForgeryCookie == null ? null : antiForgeryCookie.Value;
                AntiForgery.Validate(cookieValue, headerTokenValue);
            }
            else
            {
                AntiForgery.Validate();
            }
        }
    }
}
