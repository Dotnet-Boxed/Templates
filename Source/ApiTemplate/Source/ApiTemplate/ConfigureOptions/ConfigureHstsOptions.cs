namespace ApiTemplate.ConfigureOptions;

using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Options;

/// <summary>
/// Configures the Strict-Transport-Security HTTP header on responses. This HTTP header is only relevant if you are
/// using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate errors and
/// warnings. See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
/// http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
/// Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
/// Note: You can refer to the following article to clear the HSTS cache in your browser
/// http://classically.me/blogs/how-clear-hsts-settings-major-browsers.
/// </summary>
public class ConfigureHstsOptions : IConfigureOptions<HstsOptions>
{
    private static readonly TimeSpan OneYear = TimeSpan.FromDays(365);

    public void Configure(HstsOptions options)
    {
        // Preload the HSTS HTTP header for better security. See https://hstspreload.org/
        options.IncludeSubDomains = true;
        options.MaxAge = OneYear;
        options.Preload = true;
    }
}
