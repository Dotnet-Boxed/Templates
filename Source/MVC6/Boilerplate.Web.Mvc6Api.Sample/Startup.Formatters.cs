namespace MvcBoilerplate
{
    using Boilerplate.Web.Mvc.Formatters;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Framework.DependencyInjection;
    using Newtonsoft.Json.Serialization;

    public partial class Startup
    {
        /// <summary>
        /// Configures the input and output formatters.
        /// </summary>
        private static void ConfigureFormatters(IMvcBuilder mvcBuilder)
        {
            // Configures the JSON output formatter to use camel case property names like 'propertyName' instead of 
            // pascal case 'PropertyName' as this is the more common JavaScript/JSON style.
            mvcBuilder.AddJsonOptions(
                x => x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Adds the BSON input and output formatters using the JSON.NET serializer.
            mvcBuilder.AddBsonSerializerFormatters();

            // Adds the XML input and output formatter using the DataContractSerializer.
            mvcBuilder.AddXmlDataContractSerializerFormatters();

            // Adds the XML input and output formatter using the XmlSerializer.
            mvcBuilder.AddXmlSerializerFormatters();
        }

        /// <summary>
        /// Configures the input and output formatters.
        /// </summary>
        private static void ConfigureFormatters(MvcOptions mvcOptions)
        {
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