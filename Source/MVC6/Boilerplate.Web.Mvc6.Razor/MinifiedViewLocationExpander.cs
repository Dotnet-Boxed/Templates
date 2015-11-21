namespace Boilerplate.Web.Mvc.Razor
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc.Razor;

    public class MinifiedViewLocationExpander : IViewLocationExpander
    {
        private const string MinifiedViewExtension = ".min.cshtml";

        private static readonly IEnumerable<string> ViewLocationFormats = new[]
        {
            "/Views/{1}/{0}" + MinifiedViewExtension,
            "/Views/Shared/{0}" + MinifiedViewExtension,
        };

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (viewLocations == null)
            {
                throw new ArgumentNullException(nameof(viewLocations));
            }

            return ViewLocationFormats;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
