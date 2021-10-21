namespace GraphQLTemplate
{
    using System;
    using Boxed.Mapping;
    using GraphQLTemplate.DataLoaders;
    using GraphQLTemplate.Directives;
    using GraphQLTemplate.Mappers;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Services;
    using GraphQLTemplate.Types;
    using HotChocolate.Execution.Configuration;
    using HotChocolate.Types;
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
        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IImmutableMapper<HumanInput, Human>, HumanInputToHumanMapper>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IDroidRepository, DroidRepository>()
                .AddSingleton<IHumanRepository, HumanRepository>();

        public static IRequestExecutorBuilder AddProjectScalarTypes(this IRequestExecutorBuilder builder) =>
            builder
                // Bind a System.DateTime type to a GraphQL date type by default.
                .BindRuntimeType<DateTime, DateType>();

        public static IRequestExecutorBuilder AddProjectDirectives(this IRequestExecutorBuilder builder) =>
            builder
                .AddDirectiveType<UpperDirectiveType>()
                .AddDirectiveType<LowerDirectiveType>()
                .AddDirectiveType<IncludeDirectiveType>()
                .AddDirectiveType<SkipDirectiveType>();

        public static IRequestExecutorBuilder AddProjectDataLoaders(this IRequestExecutorBuilder builder) =>
            builder
                .AddDataLoader<IDroidDataLoader, DroidDataLoader>()
                .AddDataLoader<IHumanDataLoader, HumanDataLoader>();

        public static IRequestExecutorBuilder AddProjectTypes(this IRequestExecutorBuilder builder) =>
            builder
                .SetSchema<MainSchema>()
                .AddQueryType<QueryObject>()
#if Mutations
                .AddMutationType<MutationObject>()
#endif
#if Subscriptions
                .AddSubscriptionType<SubscriptionObject>()
#endif
                .AddType<EpisodeEnumeration>()
                .AddType<CharacterInterface>()
                .AddType<DroidObject>()
                .AddType<HumanObject>()
                .AddType<HumanInputObject>();
    }
}
