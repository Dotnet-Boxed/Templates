namespace Boilerplate.Web.Mvc
{
    using System.Web.Mvc;

    /// <summary>
    /// Represents a view engine that is used to render a Web page that uses the ASP.NET Razor syntax and C# .cshtml 
    /// files. The <see cref="RazorViewEngine"/> handles both C# and VB and looks for both .cshtml and .vbhtml files,
    /// whereas this version only looks for .cshtml files and so has a slight performance advantage.
    /// </summary>
    public class CSharpRazorViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpRazorViewEngine"/> class.
        /// </summary>
        public CSharpRazorViewEngine()
        {
            this.AreaViewLocationFormats = new string[] 
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            this.AreaMasterLocationFormats = new string[] 
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            this.AreaPartialViewLocationFormats = new string[] 
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };
            this.ViewLocationFormats = new string[] 
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            this.MasterLocationFormats = new string[] 
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            this.PartialViewLocationFormats = new string[] 
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            this.FileExtensions = new string[] { "cshtml" };
        }
    }
}