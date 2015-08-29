namespace MvcBoilerplate
{
    using System.Linq;
    using Boilerplate.Web.Mvc.Formatters;
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

            // Adds the BSON input and output formatters
            mvcOptions.AddBsonSerializerFormatter();
            // Configures the BSON output formatter to use camel case property names like 'propertyName' instead of 
            // pascal case 'PropertyName' as this is the more common JavaScript/JSON/BSON style.
            BsonOutputFormatter bsonOutputFormatter = mvcOptions.OutputFormatters
                .OfType<BsonOutputFormatter>()
                .First();
            bsonOutputFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Adds the XML input and output formatter.
            mvcOptions.AddXmlDataContractSerializerFormatter();

            // Force 204 No Content response, when returning null values.
            // Note that we are inserting this at the top of the formatters collection so we can select a formatter
            // quickly in this case.
            mvcOptions.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter());

            // Force 406 Not Acceptable responses if the media type is not supported, instead of returning the default.
            // Note that we are inserting this near the top of the formatters collection so we can select a formatter
            // quickly in this case.
            mvcOptions.OutputFormatters.Insert(1, new HttpNotAcceptableOutputFormatter());
        }
    }
}