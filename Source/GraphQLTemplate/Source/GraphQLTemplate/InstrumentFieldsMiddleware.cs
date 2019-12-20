namespace GraphQLTemplate
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GraphQL.Instrumentation;
    using GraphQL.Types;

    public class InstrumentFieldsMiddleware
    {
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods
        public async Task<object> Resolve(ResolveFieldContext context, FieldMiddlewareDelegate next)
#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
#pragma warning restore CA1822 // Mark members as static
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var metadata = new Dictionary<string, object>
            {
                { "typeName", context.ParentType.Name },
                { "fieldName", context.FieldName },
                { "path", context.Path },
                { "arguments", context.Arguments },
            };
            var path = $"{context.ParentType.Name}.{context.FieldName}";

            using (context.Metrics.Subject("field", path, metadata))
            {
                return await next(context).ConfigureAwait(false);
            }
        }
    }
}
