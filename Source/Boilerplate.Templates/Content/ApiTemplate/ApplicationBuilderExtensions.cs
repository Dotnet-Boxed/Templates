namespace ApiTemplate
{
    using System;
    using System.Linq;
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using ApiTemplate.Constants;
    using ApiTemplate.Settings;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure tools used to help with debugging the application.
        /// </summary>
        public static IApplicationBuilder UseDebugging(this IApplicationBuilder application) =>
            // Allow updates to your files in Visual Studio to be shown in the browser. You can use the Refresh
            // browser link button in the Visual Studio toolbar or Ctrl+Alt+Enter to refresh the browser.
            application.UseBrowserLink();

        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                // When a database error occurs, displays a detailed error page with full diagnostic information. It is
                // unsafe to use this in production. Uncomment this if using a database.
                // .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                // When an error occurs, displays a detailed error page with full diagnostic information.
                // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                .UseDeveloperExceptionPage();

        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(
            this IApplicationBuilder application,
            IConfigurationRoot configuration)
        {
            var cacheProfile = configuration
                .GetSection<CacheProfileSettings>()
                .CacheProfiles
                .First(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Value;
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            context.Context.ApplyCacheProfile(cacheProfile);
                        }
                    });
        }
#if (HttpsEverywhere)

        /// <summary>
        /// Adds the Strict-Transport-Security HTTP header to responses. This HTTP header is only relevant if you are
        /// using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate
        /// errors and warnings.
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
        /// http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
        /// Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
        /// Note: You can refer to the following article to clear the HSTS cache in your browser:
        /// http://classically.me/blogs/how-clear-hsts-settings-major-browsers
        /// </summary>
        public static IApplicationBuilder UseStrictTransportSecurityHttpHeader(this IApplicationBuilder application) =>
            application.UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload());
#if (PublicKeyPinning)

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
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning and
        /// https://scotthelme.co.uk/hpkp-http-public-key-pinning/
        /// </summary>
        public static IApplicationBuilder UsePublicKeyPinsHttpHeader(this IApplicationBuilder application)
        {
            // application.UseHpkp(options => options
            //     .Sha256Pins(
            //         "Base64 encoded SHA-256 hash of your first certificate e.g. cUPcTAZWKaASuYWhhneDttWpY3oBAkE3h2+soZS7sWs=",
            //         "Base64 encoded SHA-256 hash of your second backup certificate e.g. M8HztCzM3elUxkcjR2S5P4hhyBNf6lHkmjAHKhpGPWE=")
            //     .MaxAge(days: 18 * 7)
            //     .IncludeSubdomains());
            // OR
            // Use UseHpkpReportOnly instead to stop browsers blocking anything but continue reporting any violations.
            // application.UseHpkpReportOnly(...)
            return application;
        }
#endif
#endif
    }
}
