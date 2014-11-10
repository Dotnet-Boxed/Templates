namespace MvcBoilerplate
{
    using System.Web.Mvc;
    using MvcBoilerplate.Constants;
    using NWebsec.Csp;
    using NWebsec.HttpHeaders;
    using NWebsec.Mvc.HttpHeaders;
    using NWebsec.Mvc.HttpHeaders.Csp;

    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            AddSecurityFilters(filters);
            AddContentSecurityPolicyFilters(filters);
        }

        /// <summary>
        /// Several NWebsec Security Filters are added here (See 
        /// <see cref="http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html"/>
        /// and <see cref="http://nwebsec.codeplex.com/"/> for more information).
        /// </summary>
        private static void AddSecurityFilters(GlobalFilterCollection filters)
        {
            // Cache-Control: no-cache, no-store, must-revalidate
            // Expires: -1
            // Pragma: no-cache
            // Specifies whether appropriate headers to prevent browser caching should be set in the HTTP response.
            // Do not apply this attribute here globally, use it sparingly to disable caching.
            // filters.Add(new SetNoCacheHttpHeadersAttribute());   

            // X-Robots-Tag - Adds the X-Robots-Tag HTTP header. Disable robots from any action or controller this attribute is applied to.
            // filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true }); 

            // X-Content-Type-Options - Adds the X-Content-Type-Options HTTP header. Stop IE9 and below from sniffing files and overriding the Content-Type header (MIME type).
            filters.Add(new XContentTypeOptionsAttribute());

            // X-Download-Options - Adds the X-Download-Options HTTP header. When users save the page, stops them from opening it and forces a save and manual open.
            filters.Add(new XDownloadOptionsAttribute());

            // X-Frame-Options - Adds the X-Frame-Options HTTP header. Stop clickjacking by stopping the page from opening in an iframe or only allowing it from the same origin.  
            //      Deny - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to display the page when it is loaded in an iframe - but only if the iframe is from the same origin as the page.
            //      SameOrigin - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to not display the page when it is loaded in an iframe.
            //      Disabled - Specifies that the X-Frame-Options header should not be set in the HTTP response.
            filters.Add(
                new XFrameOptionsAttribute()
                {
                    Policy = XFrameOptionsPolicy.Deny
                });

            // X-XSS-Protection - Adds the X-XSS-Protection HTTP header. By default IE and Chrome try to detect XSS attacks. This will block the page if a XSS attack is detected.
            //      Note: There is a vulnerability in IE8 if this header is set which enables XSS to happen. See https://github.com/evilpacket/helmet#xss-filter-xssfilter and http://www.w3.org/TR/CSP.
            // filters.Add(new XXssProtectionAttribute() { BlockMode = true });
        }

        /// <summary>
        /// Adds the Content-Security-Policy (CSP) and/or Content-Security-Policy-Report-Only HTTP headers.
        /// This creates a whitelist from where various content in a webpage can be loaded from. (See
        /// <see cref="http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html"/> and 
        /// <see cref="http://nwebsec.codeplex.com/wikipage?title=Configuring%20Content%20Security%20Policy&referringTitle=NWebsec.Mvc"/>
        /// and <see cref="https://developer.mozilla.org/en-US/docs/Web/Security/CSP/CSP_policy_directives"/> for more information).
        /// Note: Not all browsers support this yet but most now do (See http://caniuse.com/#search=CSP for a list).
        /// Note: If you are using the 'Browser Link' feature of the Webs Essentials Visual Studio extension, it will not work
        /// if you enable CSP (See <see cref="http://webessentials.uservoice.com/forums/140520-general/suggestions/6665824-browser-link-support-for-content-security-policy"/>).
        /// </summary>
        private static void AddContentSecurityPolicyFilters(GlobalFilterCollection filters)
        {
            // Content-Security-Policy - Add the Content-Security-Policy HTTP header to enable Content-Security-Policy.
            filters.Add(new CspAttribute());
            // OR
            // Content-Security-Policy-Report-Only - Add the Content-Security-Policy-Report-Only HTTP header to enable logging of 
            //      violations without blocking them. This is good for testing CSP without enabling it.
            //      To make use of this attribute, rename all the attributes below to their ReportOnlyAttribute versions e.g. CspDefaultSrcAttribute 
            //      becomes CspDefaultSrcReportOnlyAttribute.
            // filters.Add(new CspReportOnlyAttribute());


            // Enables logging of CSP violations. See the NWebsecHttpHeaderSecurityModule_CspViolationReported method in Global.asax.cs to 
            // see where they are logged.
            filters.Add(new CspReportUriAttribute() { EnableBuiltinHandler = true });


            // default-src - Sets a default source list for a number of directives. If the other directives below are not used 
            //               then this is the default setting.
            filters.Add(
                new CspDefaultSrcAttribute()
                {
                    // Disallow everything from the same domain by default.
                    None = Source.Enable,
                    // Allow everything from the same domain by default.
                    // Self = Source.Enable
                });


            // connect-src - The connect-src directive restricts which URIs the protected resource can load using script interfaces 
            //               (Ajax Calls and Web Sockets).
            filters.Add(
                new CspConnectSrcAttribute()
                {
                    // Allow AJAX and Web Sockets to example.com.
                    // CustomSources = "example.com",
                    // Allow all AJAX and Web Sockets calls from the same domain.
                    // Self = Source.Enable
                });
            // font-src - The font-src directive restricts from where the protected resource can load fonts.
            filters.Add(
                new CspFontSrcAttribute()
                {
                    // Allow all fonts from the same domain.
                    Self = Source.Enable
                });
            // frame-src - The frame-src directive restricts from where the protected resource can embed frames.
            filters.Add(
                new CspFrameSrcAttribute()
                {
                    // Allow iFrames from example.com.
                    // CustomSources = "example.com"
                    // Allow iFrames from the same domain.
                    // Self = Source.Enable
                });
            // img-src - The img-src directive restricts from where the protected resource can load images.
            filters.Add(
                new CspImgSrcAttribute()
                {
                    // Allow images from example.com.
                    // CustomSources = "example.com"
                    // Allow images from the same domain.
                    Self = Source.Enable,
                });
            // script-src - The script-src directive restricts which scripts the protected resource can execute. The directive also controls other resources, such as XSLT style sheets, which can cause the user agent to execute script.
            filters.Add(
                new CspScriptSrcAttribute()
                {
                    // Allow scripts from the CDN's.
                    CustomSources = string.Format("{0} {1}", ContentDeliveryNetwork.Google.Domain, ContentDeliveryNetwork.Microsoft.Domain),
                    // Allow scripts from the same domain.
                    Self = Source.Enable,
                    // Allow the use of the eval() method to create code from strings. This is unsafe and can open your site up to XSS vulnerabilities.
                    // UnsafeEval = Source.Enable,
                    // Allow inline JavaScript, this is unsafe and can open your site up to XSS vulnerabilities.
                    // UnsafeInline = Source.Enable
                });
            // media-src - The media-src directive restricts from where the protected resource can load video and audio.
            filters.Add(
                new CspMediaSrcAttribute()
                {
                    // Allow audio and video from example.com.
                    // CustomSources = "example.com",
                    // Allow audio and video from the same domain.
                    // Self = Source.Enable
                });
            // object-src - The object-src directive restricts from where the protected resource can load plugins.
            filters.Add(
                new CspObjectSrcAttribute()
                {
                    // Allow plugins from example.com.
                    // CustomSources = "example.com"
                    // Allow plugins from the same domain.
                    // Self = Source.Enable
                });
            // style-src - The style-src directive restricts which styles the user applies to the protected resource.
            filters.Add(
                new CspStyleSrcAttribute()
                {
                    // Allow CSS from example.com
                    // CustomSources = "example.com"
                    // Allow CSS from the same domain.
                    Self = Source.Enable,
                    // Allow inline CSS, this is unsafe and can open your site up to XSS vulnerabilities.
                    // Note: This is currently enable because Modernizr does not support CSP and includes inline styles
                    // in its JavaScript files. This is a security hold. If you don't want to use Modernizr, 
                    // be sure to disable unsafe inline styles. For more information see:
                    // http://stackoverflow.com/questions/26532234/modernizr-causes-content-security-policy-csp-violation-errors
                    // https://github.com/Modernizr/Modernizr/pull/1263
                    UnsafeInline = Source.Enable
                });
        }
    }
}
