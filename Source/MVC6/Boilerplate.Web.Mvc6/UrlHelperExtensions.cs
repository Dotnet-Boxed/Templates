namespace Boilerplate.Web.Mvc
{
    using System;
    using Microsoft.AspNet.Hosting;
    using Microsoft.AspNet.Mvc;

    /// <summary>
    /// <see cref="UrlHelper"/> extension methods.
    /// </summary>
    public static class UrlHelperExtensions
    {
        private static IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// Configures the <see cref="IUrlHelper"/> extension methods.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name, controller name and 
        /// route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(
            this IUrlHelper url,
            string actionName, 
            string controllerName, 
            object routeValues = null)
        {
            string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
            return url.Action(actionName, controllerName, routeValues, scheme);
        }

        /// <summary>
        /// Generates a fully qualified URL to the specified content by using the specified content path. Converts a 
        /// virtual (relative) path to an application absolute path.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="contentPath">The content path.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteContent(
            this IUrlHelper url,
            string contentPath)
        {
            return new Uri(new Uri(HttpContextAccessor.HttpContext.Request.Path.Value), url.Content(contentPath)).ToString();
        }

        /// <summary>
        /// Generates a fully qualified URL to the specified route by using the route name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteRouteUrl(
            this IUrlHelper url,
            string routeName,
            object routeValues = null)
        {
            string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
            return url.RouteUrl(routeName, routeValues, scheme);
        }
    }
}