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

            // Adds the XML input and output formatter.
            mvcOptions.AddXmlDataContractSerializerFormatter();
        }
    }
}