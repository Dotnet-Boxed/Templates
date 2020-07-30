namespace GraphQLTemplate
{
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Services;
    using HotChocolate;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    internal static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();

        /// <summary>
        /// Add project data repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services with caching services added.</returns>
        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IDroidRepository, DroidRepository>()
                .AddSingleton<IHumanRepository, HumanRepository>();

        /// <summary>
        /// Add project data loaders.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services with caching services added.</returns>
        public static IServiceCollection AddProjectDataLoaders(this IServiceCollection services) =>
            services
                .AddDataLoader<IDroidDataLoader, DroidDataLoader>()
                .AddDataLoader<IHumanDataLoader, HumanDataLoader>();
    }
}
