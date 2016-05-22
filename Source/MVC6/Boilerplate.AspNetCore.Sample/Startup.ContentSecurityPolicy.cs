namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Filters;
    using MvcBoilerplate.Constants;
    using NWebsec.Mvc.HttpHeaders.Csp;

    public partial class Startup
    {
        // $Start-HttpsEverywhere$
        /// <summary>
        /// Configures the content security policy for the application.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The environment the application is running under. This can be Development,
        /// Staging or Production by default.</param>
        private static void ConfigureContentSecurityPolicy(
            IApplicationBuilder application,
            IHostingEnvironment environment)
        {
            // Require HTTPS to be used across the whole site.
            if (!environment.IsDevelopment())
            {
                // In the Development environment, different ports are being used for HTTP and HTTPS. The
                // RequireHttpsAttribute expects to use the default ports 80 for HTTP and port 443 for HTTPS and simply
                // adds an 's' onto 'http'. Therefore, we don't add this attribute under the Development environment.

                // Content-Security-Policy:upgrade-insecure-requests - Adds the 'upgrade-insecure-requests' directive to
                //      the Content-Security-Policy HTTP header. This is only relevant if you are using HTTPS. Any objects
                //      on the page using HTTP is automatically upgraded to HTTPS.
                //      See https://scotthelme.co.uk/migrating-from-http-to-https-ease-the-pain-with-csp-and-hsts/
                //      and http://www.w3.org/TR/upgrade-insecure-requests/
                application.UseCsp(x => x.UpgradeInsecureRequests());
                // OR
                // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable
                //      logging of violations without blocking them. This is good for testing CSP without enabling it. To
                //      make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions
                //      e.g. CspDefaultSrcAttribute becomes CspDefaultSrcReportOnlyAttribute.
                // application.UseCspReportOnly(x => x.UpgradeInsecureRequests());
            }
        }

        // $End-HttpsEverywhere$
        /// <summary>
        /// Adds the Content-Security-Policy (CSP) and/or Content-Security-Policy-Report-Only HTTP headers. This
        /// creates a white-list from where various content in a web page can be loaded from. (See
        /// <see cref="http://rehansaeed.com/content-security-policy-for-asp-net-mvc/"/> and
        /// <see cref="https://developer.mozilla.org/en-US/docs/Web/Security/CSP/CSP_policy_directives"/>
        /// <see cref="https://github.com/NWebsec/NWebsec/wiki"/> and for more information).
        /// Note: If you are using the 'Browser Link' feature of the Webs Essentials Visual Studio extension, it will
        /// not work if you enable CSP (See
        /// <see cref="http://webessentials.uservoice.com/forums/140520-general/suggestions/6665824-browser-link-support-for-content-security-policy"/>).
        /// Note: All of these filters can be applied to individual controllers and actions e.g. If an action requires
        /// access to content from YouTube.com, then you can add the following attribute to the action:
        /// [CspFrameSrc(CustomSources = "*.youtube.com")].
        /// </summary>
        /// <param name="environment">The environment the application is running under. This can be Development,
        /// Staging or Production by default.</param>
        private static void ConfigureContentSecurityPolicyFilters(
            IHostingEnvironment environment,
            ICollection<IFilterMetadata> filters)
        {
            // Content-Security-Policy - Add the Content-Security-Policy HTTP header to enable Content-Security-Policy.
            filters.Add(new CspAttribute());
            // OR
            // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable
            //      logging of violations without blocking them. This is good for testing CSP without enabling it. To
            //      make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions
            //      e.g. CspDefaultSrcAttribute becomes CspDefaultSrcReportOnlyAttribute.
            // filters.Add(new CspReportOnlyAttribute());


            // Enables logging of CSP violations. Register with the https://report-uri.io/ service to get a URL where
            // you can send your CSP violation reports and view them.
            // filters.Add(
            //     new CspReportUriAttribute()
            //     {
            //         ReportUris = ""
            //     });


            // default-src - Sets a default source list for a number of directives. If the other directives below are
            //               not used then this is the default setting.
            filters.Add(
                new CspDefaultSrcAttribute()
                {
                    // Disallow everything from the same domain by default.
                    None = true,
                    // Allow everything from the same domain by default.
                    // Self = true
                });


            // base-uri - This directive restricts the document base URL
            //            (See http://www.w3.org/TR/html5/infrastructure.html#document-base-url).
            filters.Add(
                new CspBaseUriAttribute()
                {
                    // Allow base URL's from example.com.
                    // CustomSources = "*.example.com",
                    // Allow base URL's from the same domain.
                    Self = false
                });
            // child-src - This directive restricts from where the protected resource can load web workers or embed
            //             frames. This was introduced in CSP 2.0 to replace frame-src. frame-src should still be used
            //             for older browsers.
            filters.Add(
                new CspChildSrcAttribute()
                {
                    // Allow web workers or embed frames from example.com.
                    // CustomSources = "*.example.com",
                    // Allow web workers or embed frames from the same domain.
                    Self = false
                });
            // connect-src - This directive restricts which URIs the protected resource can load using script interfaces
            //               (Ajax Calls and Web Sockets).
            filters.Add(
                new CspConnectSrcAttribute()
                {
                    // Allow AJAX and Web Sockets to the following sources.
                    // $Start-ApplicationInsights-On$
                    CustomSources = string.Join(
                        " ",
                        // "*.example.com",                     // Allow AJAX and Web Sockets to example.com.
                        "dc.services.visualstudio.com"),        // Allow posting data back to Application Insights.
                    // $End-ApplicationInsights-On$
                    // $Start-ApplicationInsights-Off$
                    // CustomSources = string.Join(
                    //     " ",
                    //     // CustomSources = "*.example.com",     // Allow AJAX and Web Sockets to example.com.
                    //     "dc.services.visualstudio.com"),        // Allow posting data back to Application Insights.
                    // $End-ApplicationInsights-Off$
                    // Allow all AJAX and Web Sockets calls from the same domain.
                    Self = true
                });
            // font-src - This directive restricts from where the protected resource can load fonts.
            filters.Add(
                new CspFontSrcAttribute()
                {
                    // Allow fonts from maxcdn.bootstrapcdn.com.
                    CustomSources = string.Join(
                        " ",
                        ContentDeliveryNetwork.MaxCdn.Domain),
                    // Allow all fonts from the same domain.
                    Self = true
                });
            // form-action - This directive restricts which URLs can be used as the action of HTML form elements.
            filters.Add(
                new CspFormActionAttribute()
                {
                    // Allow forms to post back to example.com.
                    // CustomSources = "*.example.com",
                    // Allow forms to post back to the same domain.
                    Self = true
                });
            // frame-src - This directive restricts from where the protected resource can embed frames.
            //             This is now deprecated in favour of child-src but should still be used for older browsers.
            filters.Add(
                new CspFrameSrcAttribute()
                {
                    // Allow iFrames from example.com.
                    // CustomSources = "*.example.com",
                    // Allow iFrames from the same domain.
                    Self = false
                });
            // frame-ancestors - This directive restricts from where the protected resource can embed frame, iframe,
            //                   object, embed or applet's.
            filters.Add(
                new CspFrameAncestorsAttribute()
                {
                    // Allow frame, iframe, object, embed or applet's from example.com.
                    // CustomSources = "*.example.com",
                    // Allow frame, iframe, object, embed or applet's from the same domain.
                    Self = false
                });
            // img-src - This directive restricts from where the protected resource can load images.
            filters.Add(
                new CspImgSrcAttribute()
                {
                    // Allow images from example.com.
                    // CustomSources = "*.example.com",
                    // Allow images from the same domain.
                    Self = true,
                });
            // script-src - This directive restricts which scripts the protected resource can execute.
            //              The directive also controls other resources, such as XSLT style sheets, which can cause the
            //              user agent to execute script.
            filters.Add(
                new CspScriptSrcAttribute()
                {
                    // Allow scripts from the CDN's.
                    CustomSources = string.Join(
                        " ",
                        // $Start-ApplicationInsights-On$
                        "az416426.vo.msecnd.net",               // Allow Application Insights to run scripts.
                        // $End-ApplicationInsights-On$
                        ContentDeliveryNetwork.Google.Domain,
                        ContentDeliveryNetwork.Microsoft.Domain),
                    // Allow scripts from the same domain.
                    Self = true,
                    // Allow the use of the eval() method to create code from strings. This is unsafe and can open your
                    // site up to XSS vulnerabilities.
                    // UnsafeEval = true,
                    // Allow in-line JavaScript, this is unsafe and can open your site up to XSS vulnerabilities.
                    // UnsafeInline = true
                });
            // media-src - This directive restricts from where the protected resource can load video and audio.
            filters.Add(
                new CspMediaSrcAttribute()
                {
                    // Allow audio and video from example.com.
                    // CustomSources = "example.com",
                    // Allow audio and video from the same domain.
                    Self = false
                });
            // object-src - This directive restricts from where the protected resource can load plug-ins.
            filters.Add(
                new CspObjectSrcAttribute()
                {
                    // Allow plug-ins from example.com.
                    // CustomSources = "example.com",
                    // Allow plug-ins from the same domain.
                    Self = false
                });
            // plugin-types - This directive restricts the set of plug-ins that can be invoked by the protected resource.
            //                You can also use the @Html.CspMediaType("application/pdf") HTML helper instead of this
            //                attribute. The HTML helper will add the media type to the CSP header.
            // filters.Add(
            //     new CspPluginTypesAttribute()
            //     {
            //         // Allow Adobe Flash and Microsoft Silverlight plug-ins
            //         MediaTypes = "application/x-shockwave-flash application/xaml+xml"
            //     });
            // style-src - This directive restricts which styles the user applies to the protected resource.
            filters.Add(
                new CspStyleSrcAttribute()
                {
                    // Allow CSS from maxcdn.bootstrapcdn.com
                    CustomSources = string.Join(
                        " ",
                        ContentDeliveryNetwork.MaxCdn.Domain),
                    // Allow CSS from the same domain.
                    Self = true,
                    // Allow in-line CSS, this is unsafe and can open your site up to XSS vulnerabilities.
                    // Note: This is currently enable because Modernizr does not support CSP and includes in-line styles
                    // in its JavaScript files. This is a security hold. If you don't want to use Modernizr, be sure to
                    // disable unsafe in-line styles. For more information See:
                    // http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
                    // https://github.com/Modernizr/Modernizr/pull/1263
                    UnsafeInline = true
                });

            if (environment.IsDevelopment())
            {
                // Allow Browser Link to work in development only.
                filters.Add(new CspConnectSrcAttribute() { CustomSources = string.Join(" ", "localhost:*", "ws://localhost:*") });
                filters.Add(new CspImgSrcAttribute() { CustomSources = "data:" });
                filters.Add(new CspScriptSrcAttribute() { CustomSources = "localhost:*" });
            }
        }
    }
}
