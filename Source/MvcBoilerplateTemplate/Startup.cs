[assembly: Microsoft.Owin.OwinStartup(typeof($safeprojectname$.Startup))]

namespace $safeprojectname$
{
    using System.Web.Mvc;
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureContainer(app);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
