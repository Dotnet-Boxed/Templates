namespace MvcBoilerplate
{
    // $Start-RedirectToCanonicalUrl$
    using Boilerplate.AspNetCore.Filters;
    // $End-RedirectToCanonicalUrl$
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    // $Start-RedirectToCanonicalUrl$
    using Microsoft.AspNetCore.Routing;
    // $End-RedirectToCanonicalUrl$
    using Microsoft.Extensions.Configuration;
    // $Start-NWebSec$
    using MvcBoilerplate.Constants;
    // $End-NWebSec$
    using MvcBoilerplate.Settings;
    // $Start-NWebSec$
    using NWebsec.AspNetCore.Mvc.HttpHeaders.Csp;
    // $End-NWebSec$

    public static partial class MvcOptionsExtensions
    {
        /// <summary>
        /// Controls how controller actions cache content in one central location.
        /// </summary>
        /// <param name="options">The ASP.NET MVC options.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are
        /// stored.</param>
        public static MvcOptions AddCacheProfilesFromConfiguration(this MvcOptions options, IConfiguration configuration)
        {
            var cacheProfileSettings = new CacheProfileSettings();
            configuration.GetSection(nameof(CacheProfileSettings)).Bind(cacheProfileSettings);

            foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
            {
                options.CacheProfiles.Add(keyValuePair);
            }

            return options;
        }
        // $Start-NWebSec$

        /// <summary>
        /// Adds the Content-Security-Policy (CSP) and/or Content-Security-Policy-Report-Only HTTP headers. This
        /// creates a white-list from where various content in a web page can be loaded from. (See
        /// http://rehansaeed.com/content-security-policy-for-asp-net-mvc/,
        /// https://developer.mozilla.org/en-US/docs/Web/Security/CSP/CSP_policy_directives and
        /// https://github.com/NWebsec/NWebsec/wiki and for more information).
        /// Note: All of these filters can be applied to individual controllers and actions e.g. If an action requires
        /// access to content from YouTube.com, then you can add the following attribute to the action:
        /// [CspFrameSrc(CustomSources = "*.youtube.com")].
        /// </summary>
        public static MvcOptions AddContentSecurityPolicyFilters(this MvcOptions options)
        {
            // Content-Security-Policy - Add the Content-Security-Policy HTTP header to enable Content-Security-Policy.
            options.Filters.Add(new CspAttribute());
            // OR
            // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable
            //      logging of violations without blocking them. This is good for testing CSP without enabling it. To
            //      make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions
            //      e.g. CspDefaultSrcAttribute becomes CspDefaultSrcReportOnlyAttribute.
            // options.Filters.Add(new CspReportOnlyAttribute());


            // Enables logging of CSP violations. Register with the https://report-uri.io/ service to get a URL where
            // you can send your CSP violation reports and view them.
            // options.Filters.Add(
            //     new CspReportUriAttribute()
            //     {
            //         ReportUris = ""
            //     });


            // default-src - Sets a default source list for a number of directives. If the other directives below are
            //               not used then this is the default setting.
            options.Filters.Add(
                new CspDefaultSrcAttribute()
                {
                    // Disallow everything from the same domain by default.
                    None = true,
                    // Allow everything from the same domain by default.
                    // Self = true
                });


            // base-uri - This directive restricts the document base URL
            //            (See http://www.w3.org/TR/html5/infrastructure.html#document-base-url).
            options.Filters.Add(
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
            options.Filters.Add(
                new CspChildSrcAttribute()
                {
                    // Allow web workers or embed frames from example.com.
                    // CustomSources = "*.example.com",
                    // Allow web workers or embed frames from the same domain.
                    Self = false
                });
            // connect-src - This directive restricts which URIs the protected resource can load using script interfaces
            //               (Ajax Calls and Web Sockets).
            options.Filters.Add(
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
            options.Filters.Add(
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
            options.Filters.Add(
                new CspFormActionAttribute()
                {
                    // Allow forms to post back to example.com.
                    // CustomSources = "*.example.com",
                    // Allow forms to post back to the same domain.
                    Self = true
                });
            // frame-src - This directive restricts from where the protected resource can embed frames.
            //             This is now deprecated in favour of child-src but should still be used for older browsers.
            options.Filters.Add(
                new CspFrameSrcAttribute()
                {
                    // Allow iFrames from example.com.
                    // CustomSources = "*.example.com",
                    // Allow iFrames from the same domain.
                    Self = false
                });
            // frame-ancestors - This directive restricts from where the protected resource can embed frame, iframe,
            //                   object, embed or applet's.
            options.Filters.Add(
                new CspFrameAncestorsAttribute()
                {
                    // Allow frame, iframe, object, embed or applet's from example.com.
                    // CustomSources = "*.example.com",
                    // Allow frame, iframe, object, embed or applet's from the same domain.
                    Self = false
                });
            // img-src - This directive restricts from where the protected resource can load images.
            options.Filters.Add(
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
            options.Filters.Add(
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
            options.Filters.Add(
                new CspMediaSrcAttribute()
                {
                    // Allow audio and video from example.com.
                    // CustomSources = "example.com",
                    // Allow audio and video from the same domain.
                    Self = false
                });
            // object-src - This directive restricts from where the protected resource can load plug-ins.
            options.Filters.Add(
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
            // options.Filters.Add(
            //     new CspPluginTypesAttribute()
            //     {
            //         // Allow Adobe Flash and Microsoft Silverlight plug-ins
            //         MediaTypes = "application/x-shockwave-flash application/xaml+xml"
            //     });
            // style-src - This directive restricts which styles the user applies to the protected resource.
            options.Filters.Add(
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

            return options;
        }

        /// <summary>
        /// Adds the Content-Security-Policy (CSP) filters used to allow Browser Link to work correctly.
        /// </summary>
        public static MvcOptions AddBrowserLinkContentSecurityPolicyFilters(this MvcOptions options)
        {
            options.Filters.Add(new CspConnectSrcAttribute() { CustomSources = string.Join(" ", "localhost:*", "ws://localhost:*") });
            options.Filters.Add(new CspImgSrcAttribute() { CustomSources = "data:" });
            options.Filters.Add(new CspScriptSrcAttribute() { CustomSources = "localhost:*" });
            return options;
        }
        // $End-NWebSec$
        // $Start-RedirectToCanonicalUrl$

        /// <summary>
        /// Adds filters which help improve search engine optimization (SEO).
        /// </summary>
        public static MvcOptions AddRedirectToCanonicalUrlFilter(this MvcOptions options, RouteOptions routeOptions)
        {
            options.Filters.Add(new RedirectToCanonicalUrlAttribute(
                 appendTrailingSlash: routeOptions.AppendTrailingSlash,
                 lowercaseUrls: routeOptions.LowercaseUrls));
            return options;
        }
        // $End-RedirectToCanonicalUrl$
        // $Start-HttpsEverywhere$

        /// <summary>
        /// Require HTTPS to be used across the whole site. Also sets a custom port to use for SSL in Development. The
        /// port number to use is taken from the launchSettings.json file which Visual Studio uses to start the
        /// application using IIS Express or the command line.
        /// </summary>
        public static MvcOptions AddRequireHttpsFilter(this MvcOptions options, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(hostingEnvironment.ContentRootPath)
                    .AddJsonFile(@"Properties\launchSettings.json")
                    .Build();

                options.SslPort = configuration.GetValue<int>("iisSettings:iisExpress:sslPort");
            }

            options.Filters.Add(new RequireHttpsAttribute());
            return options;
        }
        // $End-HttpsEverywhere$
    }
}
