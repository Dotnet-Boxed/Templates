namespace MvcBoilerplate.Framework
{
    using System.Web.Mvc;

    /// <summary>
    /// <see cref="UrlHelper"/> extension methods.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(
            this UrlHelper url,
            string actionName, 
            string controllerName, 
            object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        /// Generates a fully qualified URL to the specified route by using
        /// the route name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteRouteUrl(
            this UrlHelper url,
            string routeName,
            object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;
            return url.RouteUrl(routeName, routeValues, scheme);
        }
    }
}