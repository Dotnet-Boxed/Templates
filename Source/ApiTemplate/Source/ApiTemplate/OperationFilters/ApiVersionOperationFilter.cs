namespace ApiTemplate.OperationFilters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiVersion = context.ApiDescription.GetApiVersion();

            // If the API explorer did not capture an API version for this operation then the action must be API
            // version-neutral, so there's nothing to add.
            if (apiVersion == null)
            {
                return;
            }

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            // Note: In most applications, service authors will choose a single, consistent approach to how API
            // versioning is applied. this sample uses:
            // 1. Query string parameter method with the name "api-version".
            // 2. URL path segment with the route parameter name "api-version".
            // Unless you allow multiple API versioning methods in your app, your implementation could be simpler.

            // Consider the url path segment parameter first
            var parameter = operation.Parameters.FirstOrDefault(x => x.Name == "api-version");
            if (parameter == null)
            {
                // the only other method in this sample is by query string
                parameter = new OpenApiParameter()
                {
                    Name = "api-version",
                    Required = true,
                    In = ParameterLocation.Query,
                    Schema = new OpenApiSchema()
                    {
                        Default = new OpenApiString(apiVersion.ToString()),
                        Type = "string",
                    },
                };
                operation.Parameters.Add(parameter);
            }
            else
            {
                // Update the default value with the current API version so that the route can be invoked in the
                // "Try It!" feature.
                if (parameter.Schema == null)
                {
                    parameter.Schema = new OpenApiSchema();
                }

                parameter.Schema.Default = new OpenApiString(apiVersion.ToString());
            }

            parameter.Description = "The requested API version";
        }
    }
}
