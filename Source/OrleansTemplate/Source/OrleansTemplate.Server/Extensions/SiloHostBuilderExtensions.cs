namespace OrleansTemplate.Server
{
    using System;
    using Orleans.Hosting;

    /// <summary>
    /// <see cref="ISiloBuilder"/> extension methods.
    /// </summary>
    public static class SiloBuilderExtensions
    {
        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the silo builder.
        /// </summary>
        /// <param name="siloBuilder">The silo builder.</param>
        /// <param name="condition">If set to <c>true</c> the action is executed.</param>
        /// <param name="action">The action used to add to the silo builder.</param>
        /// <returns>The same silo builder.</returns>
        public static ISiloBuilder UseIf(
            this ISiloBuilder siloBuilder,
            bool condition,
            Func<ISiloBuilder, ISiloBuilder> action)
        {
            if (siloBuilder is null)
            {
                throw new ArgumentNullException(nameof(siloBuilder));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition)
            {
                siloBuilder = action(siloBuilder);
            }

            return siloBuilder;
        }

        /// <summary>
        /// Executes the specified action if the specified <paramref name="condition"/> is <c>true</c> which can be
        /// used to conditionally add to the silo builder.
        /// </summary>
        /// <param name="siloBuilder">The silo builder.</param>
        /// <param name="condition">If <c>true</c> is returned the action is executed.</param>
        /// <param name="action">The action used to add to the silo builder.</param>
        /// <returns>The same silo builder.</returns>
        public static ISiloBuilder UseIf(
            this ISiloBuilder siloBuilder,
            Func<ISiloBuilder, bool> condition,
            Func<ISiloBuilder, ISiloBuilder> action)
        {
            if (siloBuilder is null)
            {
                throw new ArgumentNullException(nameof(siloBuilder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (condition(siloBuilder))
            {
                siloBuilder = action(siloBuilder);
            }

            return siloBuilder;
        }
    }
}
