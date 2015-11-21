namespace Boilerplate.Web.Mvc.Filters
{
    using System;
    using Microsoft.AspNet.Antiforgery;
    using Microsoft.AspNet.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.OptionsModel;

    /// <summary>
    /// Represents an attribute that is used to prevent forgery of a request. Same as 
    /// <see cref="ValidateAntiForgeryTokenAttribute"/>, except this instance checks the HTTP Headers instead of 
    /// the form inputs for one of the tokens. The other token is expected to be in the cookie as normal. This is 
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
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateHeaderAntiForgeryTokenAttribute : Attribute, IFilterFactory, IOrderedFilter
    {
        public int Order { get; set; }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new ValidateHeaderAntiForgeryTokenAuthorizationFilter(
                ServiceProviderExtensions.GetRequiredService<IAntiforgery>(serviceProvider),
                ServiceProviderExtensions.GetRequiredService<IOptions<AntiforgeryOptions>>(serviceProvider));
        }
    }
}
