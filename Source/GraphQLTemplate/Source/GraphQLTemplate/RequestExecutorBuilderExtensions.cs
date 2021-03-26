namespace GraphQLTemplate
{
    using System;
    using HotChocolate.Execution.Configuration;

    /// <summary>
    /// <see cref="IRequestExecutorBuilder"/> extensions.
    /// </summary>
    public static class RequestExecutorBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the request execution pipeline.
        /// </summary>
        /// <param name="builder">The request executor builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the request execution pipeline.</param>
        /// <returns>The same application builder.</returns>
        public static IRequestExecutorBuilder AddIf(
            this IRequestExecutorBuilder builder,
            bool condition,
            Func<IRequestExecutorBuilder, IRequestExecutorBuilder> action)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                builder = action(builder);
            }

            return builder;
        }

        /// <summary>
        /// Executes the specified <paramref name="ifAction"/> if the specified <paramref name="condition"/> is
        /// <c>true</c>, otherwise executes the <paramref name="elseAction"/>. This can be used to conditionally add to
        /// the request execution pipeline.
        /// </summary>
        /// <param name="builder">The request executor builder.</param>
        /// <param name="condition">If set to <c>true</c> the <paramref name="ifAction"/> is executed, otherwise the
        /// <paramref name="elseAction"/> is executed.</param>
        /// <param name="ifAction">The action used to add to the request execution pipeline if the condition is
        /// <c>true</c>.</param>
        /// <param name="elseAction">The action used to add to the request execution pipeline if the condition is
        /// <c>false</c>.</param>
        /// <returns>The same application builder.</returns>
        public static IRequestExecutorBuilder AddIfElse(
            this IRequestExecutorBuilder builder,
            bool condition,
            Func<IRequestExecutorBuilder, IRequestExecutorBuilder> ifAction,
            Func<IRequestExecutorBuilder, IRequestExecutorBuilder> elseAction)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (ifAction is null)
            {
                throw new ArgumentNullException(nameof(ifAction));
            }

            if (elseAction is null)
            {
                throw new ArgumentNullException(nameof(elseAction));
            }

            if (condition)
            {
                builder = ifAction(builder);
            }
            else
            {
                builder = elseAction(builder);
            }

            return builder;
        }
    }
}
