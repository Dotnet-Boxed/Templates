namespace GraphQLTemplate
{
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddCustomMvcOptions(this IMvcBuilder builder, IConfiguration configuration) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileOptions = configuration
                        .GetRequiredSection(nameof(ApplicationOptions.CacheProfiles))
                        .Get<CacheProfileOptions>();
                    if (cacheProfileOptions != null)
                    {
                        foreach (var keyValuePair in cacheProfileOptions)
                        {
                            options.CacheProfiles.Add(keyValuePair);
                        }
                    }

                    // Remove plain text (text/plain) output formatter.
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });
    }
}
