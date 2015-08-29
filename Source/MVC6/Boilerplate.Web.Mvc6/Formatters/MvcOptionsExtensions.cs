namespace Boilerplate.Web.Mvc.Formatters
{
    using Microsoft.AspNet.Mvc;
    using Newtonsoft.Json;

    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Adds <see cref="BsonInputFormatter"/> and 
        /// <see cref="BsonOutputFormatter"/> to the
        /// input and output formatter collections respectively.
        /// </summary>
        /// <param name="options">The MvcOptions</param>
        public static void AddBsonSerializerFormatter(this MvcOptions options)
        {
            options.OutputFormatters.Add(new BsonOutputFormatter());
            options.InputFormatters.Add(new BsonInputFormatter());
        }

        /// <summary>
        /// Adds <see cref="BsonInputFormatter"/> and 
        /// <see cref="BsonOutputFormatter"/> to the
        /// input and output formatter collections respectively.
        /// </summary>
        /// <param name="options">The MvcOptions</param>
        /// <param name="jsonSerializerSettings">The <see cref="JsonSerializerSettings"/> used to configure the <see cref="JsonSerializer"/>.</param>
        public static void AddBsonSerializerFormatter(this MvcOptions options, JsonSerializerSettings jsonSerializerSettings)
        {
            options.OutputFormatters.Add(new BsonOutputFormatter(jsonSerializerSettings));
            options.InputFormatters.Add(new BsonInputFormatter(jsonSerializerSettings));
        }
    }
}