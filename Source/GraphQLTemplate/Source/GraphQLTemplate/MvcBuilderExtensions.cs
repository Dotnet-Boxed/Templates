namespace GraphQLTemplate
{
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomJsonOptions(
            this IMvcBuilder builder,
            IWebHostEnvironment webHostEnvironment) =>
            builder.AddNewtonsoftJson(
                options =>
                {
                    if (webHostEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    }

                    // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
                    // DateTime everywhere. Not doing so can cause problems with time-zones.
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

                    // Output enumeration values as strings in JSON.
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

        public static IMvcBuilder AddCustomMvcOptions(this IMvcBuilder builder, IConfiguration configuration) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    foreach (var keyValuePair in configuration
                        .GetSection(nameof(ApplicationOptions.CacheProfiles))
                        .Get<CacheProfileOptions>())
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    // Remove plain text (text/plain) output formatter.
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });
    }
}
