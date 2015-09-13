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
            // The JSON input and output formatters are added to MVC by default.
            // $Start-JsonSerializerSettings$

            // Configures the JSON output formatter to use camel case property names like 'propertyName' instead of 
            // pascal case 'PropertyName' as this is the more common JavaScript/JSON style.
            mvcBuilder.AddJsonOptions(
                x => x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            // $End-JsonSerializerSettings$
            // $Start-BsonFormatter$

            // Adds the BSON input and output formatters using the JSON.NET serializer.
            mvcBuilder.AddBsonSerializerFormatters();
            // $End-BsonFormatter$
            // $Start-XmlFormatter-DataContractSerializer$

            // Adds the XML input and output formatter using the DataContractSerializer.
            mvcBuilder.AddXmlDataContractSerializerFormatters();
            // $End-XmlFormatter-DataContractSerializer$
            // $Start-XmlFormatter-XmlSerializer$

            // Adds the XML input and output formatter using the XmlSerializer.
            mvcBuilder.AddXmlSerializerFormatters();
            // $End-XmlFormatter-XmlSerializer$
        }

        /// <summary>
        /// Configures the input and output formatters.
        /// </summary>
        private static void ConfigureFormatters(MvcOptions mvcOptions)
        {
            // $Start-NoContentFormatter$
            // Force 204 No Content response, when returning null values.
            // Note that we are inserting this at the top of the formatters collection so we can select a formatter
            // quickly in this case.
            mvcOptions.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter());
            // $End-NoContentFormatter$
            // $Start-NotAcceptableFormatter$
            // Force 406 Not Acceptable responses if the media type is not supported, instead of returning the default.
            // Note that we are inserting this near the top of the formatters collection so we can select a formatter
            // quickly in this case.
            mvcOptions.OutputFormatters.Insert(1, new HttpNotAcceptableOutputFormatter());
            // $End-NotAcceptableFormatter$
        }
    }
}