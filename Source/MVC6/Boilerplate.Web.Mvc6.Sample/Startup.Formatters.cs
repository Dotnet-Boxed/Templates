namespace MvcBoilerplate
{
    using Boilerplate.Web.Mvc.Formatters;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
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
    }
}