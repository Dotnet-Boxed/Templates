namespace MvcBoilerplate
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class Startup
    {
        /// <summary>
        /// Configures the anti-forgery tokens for better security. See:
        /// http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        private static void ConfigureAntiforgeryServices(IServiceCollection services)
        {
            services.ConfigureAntiforgery(
                antiforgeryOptions =>
                {
                    // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". This adds a little 
                    // security through obscurity and also saves sending a few characters over the wire. 
                    antiforgeryOptions.CookieName = "f";

                    // Rename the form input name from "__RequestVerificationToken" to "f" for the same reason above 
                    // e.g. <input name="__RequestVerificationToken" type="hidden" value="..." />
                    antiforgeryOptions.FormFieldName = "f";

                    // If you have enabled SSL/TLS. Uncomment this line to ensure that the Anti-Forgery cookie requires 
                    // SSL /TLS to be sent across the wire. 
                    // $Start-HttpsEverywhere$
                    // antiforgeryOptions.RequireSsl = true;
                    // $End-HttpsEverywhere$
                });
        }
    }
}
