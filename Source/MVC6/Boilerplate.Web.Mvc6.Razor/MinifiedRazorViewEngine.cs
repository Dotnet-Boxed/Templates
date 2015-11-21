namespace Boilerplate.Web.Mvc.Razor
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc.Razor;
    using Microsoft.Extensions.OptionsModel;

    /// <summary>
    /// Inherits from <see cref="RazorViewEngine"/> but looks for minified .min.cshtml files, instead of .cshtml files.
    /// </summary>
    public class MinifiedRazorViewEngine : RazorViewEngine
    {
        private const string MinifiedViewExtension = ".min.cshtml";

        private static readonly IEnumerable<string> _viewLocationFormats = new[]
        {
            "/Views/{1}/{0}" + MinifiedViewExtension,
            "/Views/Shared/{0}" + MinifiedViewExtension,
        };

        private static readonly IEnumerable<string> _areaViewLocationFormats = new[]
        {
            "/Areas/{2}/Views/{1}/{0}" + MinifiedViewExtension,
            "/Areas/{2}/Views/Shared/{0}" + MinifiedViewExtension,
            "/Views/Shared/{0}" + MinifiedViewExtension,
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="MinifiedRazorViewEngine" /> class.
        /// </summary>
        /// <param name="pageFactory">The page factory used for creating <see cref="IRazorPage"/> instances.</param>
        public MinifiedRazorViewEngine(
            IRazorPageFactory pageFactory,
            IRazorViewFactory viewFactory,
            IOptions<RazorViewEngineOptions> optionsAccessor,
            IViewLocationCache viewLocationCache)
            : base(pageFactory, viewFactory, optionsAccessor, viewLocationCache)
        {
        }

        /// <summary>
        /// Gets the locations where this instance of <see cref="MinifiedRazorViewEngine"/> will search for views.
        /// </summary>
        /// <remarks>
        /// The locations of the views returned from controllers that do not belong to an area.
        /// Locations are composite format strings (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx),
        /// which contains following indexes:
        /// {0} - Action Name
        /// {1} - Controller Name
        /// The values for these locations are case-sensitive on case-sensitive file systems.
        /// For example, the view for the <c>Test</c> action of <c>HomeController</c> should be located at
        /// <c>/Views/Home/Test.min.cshtml</c>. Locations such as <c>/views/home/test.min.cshtml</c> would not be discovered
        /// </remarks>
        public override IEnumerable<string> ViewLocationFormats
        {
            get { return _viewLocationFormats; }
        }

        /// <summary>
        /// Gets the locations where this instance of <see cref="MinifiedRazorViewEngine"/> will search for views within an
        /// area.
        /// </summary>
        /// <remarks>
        /// The locations of the views returned from controllers that belong to an area.
        /// Locations are composite format strings (see http://msdn.microsoft.com/en-us/library/txafckwd.aspx),
        /// which contains following indexes:
        /// {0} - Action Name
        /// {1} - Controller Name
        /// {2} - Area name
        /// The values for these locations are case-sensitive on case-sensitive file systems.
        /// For example, the view for the <c>Test</c> action of <c>HomeController</c> should be located at
        /// <c>/Views/Home/Test.min.cshtml</c>. Locations such as <c>/views/home/test.min.cshtml</c> would not be discovered
        /// </remarks>
        public override IEnumerable<string> AreaViewLocationFormats
        {
            get { return _areaViewLocationFormats; }
        }
    }
}