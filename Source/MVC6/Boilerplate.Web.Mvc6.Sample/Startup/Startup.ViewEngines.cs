namespace MvcBoilerplate
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Mvc.OptionDescriptors;

    public partial class Startup
    {
        /// <summary>
        /// Configures the view engines. By default, Asp.Net MVC includes the <see cref="WebFormsViewEngine"/> and 
        /// <see cref="RazorViewEngine"/>. You can remove view engines you are not using here for better performance.
        /// </summary>
        /// <param name="viewEngines">The engines used to render the views.</param>
        private static void ConfigureViewEngines(IList<ViewEngineDescriptor> viewEngines)
        {
            // TODO: Remove Web Forms view engine when it is added by Microsoft in a later version of ASP.NET 5.
            //ViewEngineDescriptor webFormsViewEngine = viewEngines
            //    .FirstOrDefault(x => x.OptionType == typeof(WebFormsViewEngine));
            //if (webFormsViewEngine != null)
            //{
            //    viewEngines.Remove(webFormsViewEngine);
            //}
        }
    }
}
