namespace Boilerplate.Web.Mvc.Filters
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Antiforgery;
    using Microsoft.AspNet.Http;
    using Microsoft.AspNet.Mvc.Filters;
    using Microsoft.Extensions.OptionsModel;

    /// <summary>
    /// Represents a filter that is used to prevent forgery of a request. This instance checks the HTTP Headers instead 
    /// of the form inputs for one of the tokens. The other token is expected to be in the cookie as normal. This is 
    /// useful for performing Ajax requests. See 
    /// http://stackoverflow.com/questions/4074199/jquery-ajax-calls-and-the-html-antiforgerytoken.
    /// </summary>
    /// <example>
    /// On the client side, when doing an Ajax request from jQuery, the anti-forgery token HTTP header can be added like so:
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
    /// On the server side, you simply need to add the <see cref="ValidateHeaderAntiForgeryTokenAttribute"/> attribute 
    /// to the controller or action the same way you would use <see cref="ValidateAntiForgeryTokenAttribute"/>.
    /// </example>
    internal sealed class ValidateHeaderAntiForgeryTokenAuthorizationFilter : IAsyncAuthorizationFilter
    {
        #region Fields
        
        /// <summary>
        /// The name of the request verification token HTTP header i.e. X-RequestVerificationToken.
        /// </summary>
        public const string RequestVerificationTokenHttpHeaderName = "X-RequestVerificationToken";

        private readonly IAntiforgery antiforgery;
        private readonly string antiForgeryCookieName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateHeaderAntiForgeryTokenAuthorizationFilter" /> class.
        /// </summary>
        /// <param name="antiforgery">The anti-forgery system.</param>
        /// <param name="antiforgeryOptions">The anti-forgery options.</param>
        public ValidateHeaderAntiForgeryTokenAuthorizationFilter(
            IAntiforgery antiforgery,
            IOptions<AntiforgeryOptions> antiforgeryOptions)
        {
            if (antiforgery == null)
            {
                throw new ArgumentNullException(nameof(antiforgery));
            }

            if (antiforgeryOptions == null)
            {
                throw new ArgumentNullException(nameof(antiforgeryOptions));
            }

            this.antiforgery = antiforgery;
            this.antiForgeryCookieName = antiforgeryOptions.Value.CookieName;
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="context">The filter context.</param>
        /// <returns>A task representing this function.</returns>
        /// <exception cref="System.ArgumentNullException">The <paramref name="context"/> parameter is <c>null</c>.</exception>
        public Task OnAuthorizationAsync(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            HttpRequest request = context.HttpContext.Request;
            string headerTokenValue = request.Headers[RequestVerificationTokenHttpHeaderName];

            // Ajax POSTs using jquery have a header set that defines the token.
            // However using unobtrusive ajax the token is still submitted normally in the form.
            // if the header is present then use it, else fall back to processing the form like normal.
            if (headerTokenValue != null)
            {
                string antiForgeryCookieValue = request.Cookies[this.antiForgeryCookieName];
                this.antiforgery.ValidateTokens(context.HttpContext, new AntiforgeryTokenSet(headerTokenValue, antiForgeryCookieValue));
                return Task.FromResult<object>(null);
            }
            else
            {
                return this.antiforgery.ValidateRequestAsync(context.HttpContext);
            }
        } 

        #endregion
    }
}
