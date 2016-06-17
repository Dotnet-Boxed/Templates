namespace MvcBoilerplate
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.CookiePolicy;

    public partial class Startup
    {
        /// <summary>
        /// Configure default cookie settings for the application which are more secure by default.
        /// </summary>
        /// <param name="application">The application.</param>
        private static void UseCookiePolicy(IApplicationBuilder application)
        {
            application.UseCookiePolicy(
                new CookiePolicyOptions()
                {
                    // Ensure that external script cannot access the cookie.
                    HttpOnly = HttpOnlyPolicy.Always,
                    // $Start-HttpsEverywhere-On$
                    // Ensure that the cookie can only be transported over HTTPS.
                    Secure = SecurePolicy.Always
                    // $End-HttpsEverywhere-On$
                    // $Start-HttpsEverywhere-Off$
                    // Ensure that the cookie can only be transported over the same scheme as the request.
                    // Secure = SecurePolicy.SameAsRequest
                    // $End-HttpsEverywhere-Off$
                });
        }
    }
}
