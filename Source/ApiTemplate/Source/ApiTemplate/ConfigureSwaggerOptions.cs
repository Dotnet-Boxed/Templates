namespace ApiTemplate
{
    using ApiTemplate.OperationFilters;
    using Boxed.AspNetCore.Swagger;
    using Boxed.AspNetCore.Swagger.OperationFilters;
    using Boxed.AspNetCore.Swagger.SchemaFilters;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
            this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            options.DescribeAllParametersInCamelCase();
            options.EnableAnnotations();

            // Add the XML comment file for this assembly, so its contents can be displayed.
            options.IncludeXmlCommentsIfExists(typeof(Startup).Assembly);

#if Versioning
            options.OperationFilter<ApiVersionOperationFilter>();
#endif
            options.OperationFilter<ClaimsOperationFilter>();
            options.OperationFilter<ForbiddenResponseOperationFilter>();
            options.OperationFilter<UnauthorizedResponseOperationFilter>();

            // Show a default and example model for JsonPatchDocument<T>.
            options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

#if Versioning
            foreach (var apiVersionDescription in this.provider.ApiVersionDescriptions)
            {
                var info = new OpenApiInfo()
                {
                    Title = AssemblyInformation.Current.Product,
                    Description = apiVersionDescription.IsDeprecated ?
                        $"{AssemblyInformation.Current.Description} This API version has been deprecated." :
                        AssemblyInformation.Current.Description,
                    Version = apiVersionDescription.ApiVersion.ToString(),
                };
                options.SwaggerDoc(apiVersionDescription.GroupName, info);
            }
#else
            var info = new Info()
            {
                Title = AssemblyInformation.Current.Product,
                Description = AssemblyInformation.Current.Description,
                Version = "v1"
            };
            options.SwaggerDoc("v1", info);
#endif
        }
    }
}
