namespace ApiTemplate.ConfigureOptions;

#if Versioning
using Microsoft.AspNetCore.Mvc.ApiExplorer;
#endif
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

public class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
{
#if Versioning
    private readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

    public ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) =>
        this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;

#endif
    public void Configure(SwaggerUIOptions options)
    {
        // Set the Swagger UI browser document title.
        options.DocumentTitle = AssemblyInformation.Current.Product;
        // Set the Swagger UI to render at '/'.
        options.RoutePrefix = string.Empty;

        options.DisplayOperationId();
        options.DisplayRequestDuration();

#if Versioning
        foreach (var apiVersionDescription in this.apiVersionDescriptionProvider
            .ApiVersionDescriptions
            .OrderByDescending(x => x.ApiVersion))
        {
            options.SwaggerEndpoint(
                $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                $"Version {apiVersionDescription.ApiVersion}");
        }
#else
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Version 1");
#endif
    }
}
