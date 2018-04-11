namespace ApiTemplate
{
    using ApiTemplate.Queries;
    using ApiTemplate.Repositories;
    using ApiTemplate.Schemas;
    using ApiTemplate.Types;
    using GraphQL.Server.Transports.WebSockets;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ICarRepository, CarRepository>()
                .AddSingleton<IPersonRepository, PersonRepository>();

        /// <summary>
        /// Add project GraphQL types.
        /// </summary>
        public static IServiceCollection AddProjectGraphQLTypes(this IServiceCollection services) =>
            services
                .AddSingleton<CarType>()
                .AddSingleton<GenderType>()
                .AddSingleton<PersonType>();

        /// <summary>
        /// Add project GraphQL query types.
        /// </summary>
        public static IServiceCollection AddProjectGraphQLQueries(this IServiceCollection services) =>
            services
                .AddSingleton<CarsQuery>();

        /// <summary>
        /// Add project GraphQL schema and web socket types.
        /// </summary>
        public static IServiceCollection AddProjectGraphQLSchemas(this IServiceCollection services) =>
            services
                .AddSingleton<CarsSchema>()
                .AddGraphQLWebSocket<CarsSchema>();
    }
}
