[assembly: Microsoft.Owin.OwinStartup(typeof($safeprojectname$.Startup))]

namespace $safeprojectname$
{
    using System.Web.Mvc;
    using NWebsec.Owin;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Strict-Transport-Security - Adds the Strict-Transport-Security HTTP header to responses.
            //      This HTTP header is only relevant if you are using TLS. It ensures that content is loaded over 
            //      HTTPS and refuses to connect in case of certificate errors and warnings. NWebSec currently does 
            //      not support an MVC filter that can be applied globally. Instead we can use Owin (Using the 
            //      added NWebSec.Owin NuGet package) to apply it. See 
            //      https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security
            // app.UseHsts(options => options.MaxAge(days: 30).IncludeSubdomains().Preload());

            // Public-Key-Pins - Adds the Public-Key-Pins HTTP header to responses.
            //      This HTTP header is only relevant if you are using TLS. It stops man-in-the-middle attacks by 
            //      telling browsers exactly which TLS certificate you expect.
            //      Note: The current specification requires including a second pin for a backup key which isn't yet 
            //      used in production. This allows for changing the server's public key without breaking accessibility 
            //      for clients that have already noted the pins. This is important for example when the former key 
            //      gets compromised. See 
            //      https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning
            // app.UseHpkp(options => options
            //     .Sha256Pins(
            //         "Base64 encoded SHA-256 hash of your first certificate e.g. cUPcTAZWKaASuYWhhneDttWpY3oBAkE3h2+soZS7sWs=",
            //         "Base64 encoded SHA-256 hash of your second backup certificate e.g. M8HztCzM3elUxkcjR2S5P4hhyBNf6lHkmjAHKhpGPWE=")
            //     .MaxAge(days: 30)
            //     .IncludeSubdomains());

            ConfigureContainer(app);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
