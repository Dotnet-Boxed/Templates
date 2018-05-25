namespace ApiTemplate.OperationFilters
{
    using System;
    using System.Collections.Generic;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Adds a Swashbuckle <see cref="NonBodyParameter"/> to all operations with a description of the X-Correlation-ID
    /// HTTP header and a default GUID value.
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class CorrelationIdOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            operation.Parameters.Add(
                new NonBodyParameter()
                {
                    Default = Guid.NewGuid(),
                    Description = "Used to uniquely identify the HTTP request. This ID is used to correlate the HTTP request between a client and server.",
                    In = "header",
                    Name = "X-Correlation-ID",
                    Required = false,
                    Type = "string",
                });
        }
    }
}
