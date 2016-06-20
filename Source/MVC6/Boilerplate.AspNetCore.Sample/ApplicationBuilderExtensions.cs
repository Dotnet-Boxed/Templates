namespace MvcBoilerplate
{
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.CookiePolicy;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure tools used to help with debugging the application.
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseDebugging(this IApplicationBuilder application)
        {
            // Allow updates to your files in Visual Studio to be shown in the browser. You can use the Refresh
            // browser link button in the Visual Studio toolbar or Ctrl+Alt+Enter to refresh the browser.
            // NOTE: Browser link has a bug in RC2 that causes rendering issues (See https://github.com/aspnet/Mvc/issues/4671).
            // application.UseBrowserLink();

            // Browse to /runtimeinfo to see information about the runtime that is being used and the packages that
            // are included in the application. See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
            return application.UseRuntimeInfoPage();
        }

        /// <summary>
        /// Configure default cookie settings for the application which are more secure by default.
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseCookiePolicy(this IApplicationBuilder application)
        {
            return application.UseCookiePolicy(
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
        // $Start-HttpsEverywhere-On$

        /// <summary>
        /// Adds the 'upgrade-insecure-requests' directive to the Content-Security-Policy HTTP header. This is only
        /// relevant if you are using HTTPS. Any objects on the page using HTTP are automatically upgraded to HTTPS.
        /// See https://scotthelme.co.uk/migrating-from-http-to-https-ease-the-pain-with-csp-and-hsts/ and
        /// http://www.w3.org/TR/upgrade-insecure-requests/
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseCspUpgradeInsecureRequestsHttpHeader(this IApplicationBuilder application)
        {
            return application.UseCsp(x => x.UpgradeInsecureRequests());
            // OR
            // use the Content-Security-Policy-Report-Only HTTP header to enable logging of violations without blocking
            // them. This is good for testing CSP without enabling it. To make use of this attribute, rename all the
            // attributes below to their ReportOnlyAttribute versions
            // e.g. CspDefaultSrcAttribute becomes CspDefaultSrcReportOnlyAttribute.
            // application.UseCspReportOnly(x => x.UpgradeInsecureRequests());
        }
        // $End-HttpsEverywhere-On$

        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application)
        {
            // When a database error occurs, displays a detailed error page with full diagnostic information. It is
            // unsafe to use this in production. Uncomment this if using a database.
            // application.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);

            // When an error occurs, displays a detailed error page with full diagnostic information.
            // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
            return application.UseDeveloperExceptionPage();
        }

        /// <summary>
        /// Adds user friendly error pages.
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseErrorPages(this IApplicationBuilder application)
        {
            // Add error handling middle-ware which handles all HTTP status codes from 400 to 599 by re-executing
            // the request pipeline for the following URL. '{0}' is the HTTP status code e.g. 404.
            application.UseStatusCodePagesWithReExecute("/error/{0}/");

            // Returns a 500 Internal Server Error response when an unhandled exception occurs.
            application.UseInternalServerErrorOnException();
            // $Start-HttpException$

            // Allows the use of HttpException as an alternative method of returning an error result.
            application.UseHttpException();
            // $End-HttpException$

            return application;
        }
        // $Start-HttpsEverywhere-On$

        /// <summary>
        /// Adds the Strict-Transport-Security HTTP header to responses. This HTTP header is only relevant if you are
        /// using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate
        /// errors and warnings.
        /// Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
        /// Note: You can view preloaded HSTS domains in Chrome here: chrome://net-internals/#hsts
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
        /// http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UseStrictTransportSecurityHttpHeader(this IApplicationBuilder application)
        {
            return application.UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload());
        }

        /// <summary>
        /// Adds the Public-Key-Pins HTTP header to responses. This HTTP header is only relevant if you are using TLS.
        /// It stops man-in-the-middle attacks by telling browsers exactly which TLS certificate you expect.
        /// Note: The current specification requires including a second pin for a backup key which isn't yet used in
        /// production. This allows for changing the server's public key without breaking accessibility for clients
        /// that have already noted the pins. This is important for example when the former key gets compromised.
        /// Note: You can use the ReportUri option to provide browsers a URL to post JSON violations of the HPKP
        /// policy. Note that the report URI must not be this site as a violation would mean that the site is blocked.
        /// You must use a separate domain using HTTPS to report to. Consider using this service:
        /// https://report-uri.io/ for this purpose.
        /// Note: You can change UseHpkp to UseHpkpReportOnly to stop browsers blocking anything but continue reporting
        /// any violations.
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning and
        /// https://scotthelme.co.uk/hpkp-http-public-key-pinning/
        /// </summary>
        /// <param name="application">The application.</param>
        public static IApplicationBuilder UsePublicKeyPinsHttpHeader(this IApplicationBuilder application)
        {
            // application.UseHpkp(options => options
            //     .Sha256Pins(
            //         "Base64 encoded SHA-256 hash of your first certificate e.g. cUPcTAZWKaASuYWhhneDttWpY3oBAkE3h2+soZS7sWs=",
            //         "Base64 encoded SHA-256 hash of your second backup certificate e.g. M8HztCzM3elUxkcjR2S5P4hhyBNf6lHkmjAHKhpGPWE=")
            //     .MaxAge(days: 18 * 7)
            //     .IncludeSubdomains());
            return application;
        }
        // $End-HttpsEverywhere-On$
    }
}
