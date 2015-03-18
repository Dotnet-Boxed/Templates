[assembly: Microsoft.Owin.OwinStartup(typeof($safeprojectname$.Startup))]

namespace $safeprojectname$
{
    using System.Web.Mvc;
    using Owin;
    using NWebsec.Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Strict-Transport-Security - Adds the Strict-Transport-Security HTTP header to responses.
            //      This HTTP header is only relevant if you are using SSL. It ensures that content is loaded over 
            //      HTTPS and refuses to connect in case of certificate errors and warnings. NWebSec currently does 
            //      not support an MVC filter that can be applied globally. Instead we can use Owin (Using the 
            //      added NWebSec.Owin NuGet package) to apply it.
            //      See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security#Preloading_Strict_Transport_Security
            // app.UseHsts(options => options.MaxAge(days: 30).IncludeSubdomains().Preload());
            ConfigureContainer(app);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
