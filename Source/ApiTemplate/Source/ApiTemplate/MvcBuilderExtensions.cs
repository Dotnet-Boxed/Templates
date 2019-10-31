namespace ApiTemplate
{
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using ApiTemplate.Options;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    public static class MvcBuilderExtensions
    {
        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcBuilder AddCustomJsonOptions(
            this IMvcBuilder builder,
            IWebHostEnvironment webHostEnvironment) =>
            builder.AddJsonOptions(
                options =>
                {
                    var jsonSerializerOptions = options.JsonSerializerOptions;
                    if (webHostEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        jsonSerializerOptions.WriteIndented = true;
                    }

                    jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

        public static IMvcBuilder AddCustomMvcOptions(this IMvcBuilder builder) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileOptions = builder
                        .Services
                        .BuildServiceProvider()
                        .GetRequiredService<CacheProfileOptions>();
                    foreach (var keyValuePair in cacheProfileOptions)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    // Remove plain text (text/plain) output formatter.
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Add support for de-serializing JsonPatchDocument<T>.
                    options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());

                    var jsonInputFormatterMediaTypes = options
                        .InputFormatters
                        .OfType<SystemTextJsonInputFormatter>()
                        .First()
                        .SupportedMediaTypes;
                    var jsonOutputFormatterMediaTypes = options
                        .OutputFormatters
                        .OfType<SystemTextJsonOutputFormatter>()
                        .First()
                        .SupportedMediaTypes;

                    // Add Problem Details media type (application/problem+json) to the JSON output formatters.
                    // See https://tools.ietf.org/html/rfc7807
                    jsonOutputFormatterMediaTypes.Insert(0, ContentType.ProblemJson);

                    // Add RESTful JSON media type (application/vnd.restful+json) to the JSON input and output formatters.
                    // See http://restfuljson.org/
                    jsonInputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);
                    jsonOutputFormatterMediaTypes.Insert(0, ContentType.RestfulJson);

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });

        /// <summary>
        /// Gets the JSON patch input formatter. The <see cref="JsonPatchDocument{T}"/> does not support the new
        /// System.Text.Json API's for de-serialization. You must use Newtonsoft.Json instead. See
        /// https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.0#jsonpatch-addnewtonsoftjson-and-systemtextjson
        /// </summary>
        /// <returns>The JSON patch input formatter using Newtonsoft.Json.</returns>
        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services;
            var serviceProvider = services.BuildServiceProvider();
            var mvcOptions = serviceProvider.GetRequiredService<IOptions<MvcOptions>>().Value;
            return mvcOptions.InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }
    }
}
