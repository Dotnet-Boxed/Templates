namespace $safeprojectname$
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using $safeprojectname$.Framework;
    using $safeprojectname$.Services;
    using NWebsec.Csp;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // By Default, Asp.Net MVC renders with an aspx engine and a razor engine. This only uses the RazorViewEngine.
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Handles the Content Security Policy (CSP) violation errors. For more information see FilterConfig.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CspViolationReportEventArgs"/> instance containing the event data.</param>
        protected void NWebsecHttpHeaderSecurityModule_CspViolationReported(object sender, CspViolationReportEventArgs e)
        {
            // Log the Content Security Policy (CSP) violation.
            CspViolationException exception = new CspViolationException(e.ViolationReport);
            DependencyResolver.Current.GetService<ILoggingService>().Log(exception);
        }
    }
}
