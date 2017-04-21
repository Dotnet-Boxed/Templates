namespace ApiTemplate.OperationFilters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ApiVersionOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiVersion = context.ApiDescription.GetApiVersion();

            // If the api explorer did not capture an API version for this operation then the action must be API
            // version-neutral, so there's nothing to add.
            if (apiVersion == null)
            {
                return;
            }

            var parameters = operation.Parameters;

            if (parameters == null)
            {
                operation.Parameters = parameters = new List<IParameter>();
            }

            // Note: In most applications, service authors will choose a single, consistent approach to how API
            // versioning is applied. this sample uses:
            // 1. Query string parameter method with the name "api-version".
            // 2. URL path segment with the route parameter name "api-version".
            // Unless you allow multiple API versioning methods in your app, your implementation could be simpler.

            // consider the url path segment parameter first
            var parameter = parameters.FirstOrDefault(p => p.Name == "api-version");
            if (parameter == null)
            {
                // the only other method in this sample is by query string
                parameter = new NonBodyParameter()
                {
                    Name = "api-version",
                    Required = true,
                    Default = apiVersion.ToString(),
                    In = "query",
                    Type = "string"
                };
                parameters.Add(parameter);
            }
            else if (parameter is NonBodyParameter pathParameter)
            {
                // Update the default value with the current API version so that the route can be invoked in the
                // "Try It!" feature.
                pathParameter.Default = apiVersion.ToString();
            }

            parameter.Description = "The requested API version";
        }
    }
}
