namespace GraphQLTemplate
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQL;
    using GraphQL.Execution;
    using GraphQL.Instrumentation;
    using GraphQL.Server;
    using GraphQL.Server.Internal;
    using GraphQL.Types;
    using GraphQL.Validation;
    using Microsoft.Extensions.Options;

    public class InstrumentingGraphQLExecutor<TSchema> : DefaultGraphQLExecuter<TSchema>
        where TSchema : ISchema
    {
        private readonly GraphQLOptions options;

        public InstrumentingGraphQLExecutor(
            TSchema schema,
            IDocumentExecuter documentExecuter,
            IOptions<GraphQLOptions> options,
            IEnumerable<IDocumentExecutionListener> listeners,
            IEnumerable<IValidationRule> validationRules)
            : base(schema, documentExecuter, options, listeners, validationRules) =>
            this.options = options.Value;

        public override async Task<ExecutionResult> ExecuteAsync(
            string operationName,
            string query,
            Inputs variables,
            object context,
            CancellationToken cancellationToken = default)
        {
            var result = await base.ExecuteAsync(operationName, query, variables, context, cancellationToken);

            if (this.options.EnableMetrics)
            {
                // Add instrumentation data showing how long field resolvers take to execute to the JSON response in
                // Apollo Tracing format. Apollo Engine can use the Apollo Tracing data to produce nice charts showing this
                // information. See https://www.apollographql.com/engine/
                result.EnrichWithApolloTracing(DateTime.UtcNow);
            }

            return result;
        }

        protected override ExecutionOptions GetOptions(
            string operationName,
            string query,
            Inputs variables,
            object context,
            CancellationToken cancellationToken)
        {
            var options = base.GetOptions(operationName, query, variables, context, cancellationToken);

            if (this.options.EnableMetrics)
            {
                // Add instrumentation to measure how long field resolvers take to execute.
                options.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
            }

            return options;
        }
    }
}
