namespace MvcBoilerplate
{
    using System.Linq;
    using Microsoft.AspNet.Mvc;
    using Newtonsoft.Json.Serialization;

    public partial class Startup
    {
        /// <summary>
        /// Configures the input and output formatters.
        /// </summary>
        /// <param name="outputFormatters">A collection of output formatters.</param>
        private static void ConfigureFormatters(MvcOptions mvcOptions)
        {
            // Configures the JSON output formatter to use camel case property names like 'propertyName' instead of 
            // pascal case 'PropertyName' as this is the more common JavaScript/JSON style.
            JsonOutputFormatter jsonOutputFormatter = mvcOptions.OutputFormatters
                .OfType<JsonOutputFormatter>()
                .First();
            jsonOutputFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Add the XML input and output formatters. 
            // See: http://www.strathweb.com/2015/04/asp-net-mvc-6-formatters-xml-browser-requests/.
            mvcOptions.AddXmlDataContractSerializerFormatter();

            // TODO: Add the BSON input and output formatters.

            // Force 204 No Content response, when returning null values.
            mvcOptions.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter());

            // Force 406 Not Acceptable responses if the media type is not supported, instead of returning the default.
            mvcOptions.OutputFormatters.Insert(0, new HttpNotAcceptableOutputFormatter());
        }
    }
}