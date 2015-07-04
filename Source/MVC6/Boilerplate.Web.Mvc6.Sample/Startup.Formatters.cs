namespace MvcBoilerplate
{
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using Newtonsoft.Json.Serialization;

    public partial class Startup
    {
        /// <summary>
        /// Configures the JSON output formatter to use camel case property names like 'propertyName' instead of pascal
        /// case 'PropertyName' as this is the more common JavaScript/JSON style. Also adds the XML input and output
        /// formatters. See: http://www.strathweb.com/2015/04/asp-net-mvc-6-formatters-xml-browser-requests/.
        /// </summary>
        /// <param name="outputFormatters">A collection of output formatters.</param>
        private static void ConfigureFormatters(MvcOptions mvcOptions)
        {
            JsonOutputFormatter jsonOutputFormatter = mvcOptions.OutputFormatters
                .OfType<JsonOutputFormatter>()
                .First();
            jsonOutputFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            mvcOptions.AddXmlDataContractSerializerFormatter();
        }
    }
}