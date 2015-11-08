namespace Boilerplate.Web.Mvc
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.ApplicationModels;

    /// <summary>
    /// Allows you to set an application wide route prefix.
    /// See http://www.strathweb.com/2015/10/global-route-prefixes-with-attribute-routing-in-asp-net-5-and-mvc-6/
    /// </summary>
    /// <example>
    /// public void ConfigureServices(IServiceCollection services)
    /// {
    ///     services.AddMvc(x => x.Conventions.Insert(0, new RouteConvention("api")));
    /// }
    /// </example>
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly string template;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteConvention"/> class.
        /// </summary>
        /// <param name="template">The route template.</param>
        public RouteConvention(string template)
        {
            this.template = template;
        }

        /// <summary>
        /// Called to apply the convention to the <see cref="ApplicationModel"/>.
        /// </summary>
        /// <param name="application"> The<see cref="ApplicationModel"/>.</param>
        public virtual void Apply(ApplicationModel application)
        {
            var centralPrefix = new AttributeRouteModel(new RouteAttribute(template));
            foreach (var controller in application.Controllers)
            {
                if (controller.AttributeRoutes.Count > 0)
                {
                    for (var i = 0; i < controller.AttributeRoutes.Count; ++i)
                    {
                        controller.AttributeRoutes[i] = AttributeRouteModel.CombineAttributeRouteModel(
                            centralPrefix,
                            controller.AttributeRoutes[i]);
                    }
                }
                else
                {
                    controller.AttributeRoutes.Add(centralPrefix);
                }
            }
        }
    }
}
