namespace GraphQLTemplate
{
    using System;
    using System.IO.Compression;
    using System.Linq;
    using Boxed.AspNetCore;
#if (CorrelationId)
    using CorrelationId;
#endif
    using GraphQL;
    using GraphQL.DataLoader;
    using GraphQL.Server.Transports.AspNetCore;
    using GraphQL.Server.Transports.Subscriptions.Abstractions;
    using GraphQL.Types.Relay;
#if (CORS)
    using GraphQLTemplate.Constants;
#endif
    using GraphQLTemplate.Options;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods which extend ASP.NET Core services.
    /// </summary>
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddCorrelationIdFluent(this IServiceCollection services)
        {
            services.AddCorrelationId();
            return services;
        }

        /// <summary>
        /// Configures caching for the application. Registers the <see cref="IDistributedCache"/> and
        /// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="IDistributedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
        /// otherwise.
        /// </summary>
        public static IServiceCollection AddCustomCaching(this IServiceCollection services) =>
            services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache();
                // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
                // override any previously registered IDistributedCache service.
                // Redis is a very fast cache provider and the recommended distributed cache provider.
                // .AddDistributedRedisCache(options => { ... });
                // Uncomment the following line to use the Microsoft SQL Server implementation of IDistributedCache.
                // Note that this would require setting up the session state database.
                // Redis is the preferred cache implementation but you can use SQL Server if you don't have an alternative.
                // .AddSqlServerCache(
                //     x =>
                //     {
                //         x.ConnectionString = "Server=.;Database=ASPNET5SessionState;Trusted_Connection=True;";
                //         x.SchemaName = "dbo";
                //         x.TableName = "Sessions";
                //     });

        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptions{T}"/> objects to the services collection.
        /// </summary>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                // Adds IOptions<ApplicationOptions> and ApplicationOptions to the services container.
                .Configure<ApplicationOptions>(configuration)
                .AddSingleton(x => x.GetRequiredService<IOptions<ApplicationOptions>>().Value)
                // Adds IOptions<CacheProfileOptions> and CacheProfileOptions to the services container.
                .Configure<CacheProfileOptions>(configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)))
                .AddSingleton(x => x.GetRequiredService<IOptions<CacheProfileOptions>>().Value)
                // Adds IOptions<CompressionOptions> and CompressionOptions to the services container.
                .Configure<CompressionOptions>(configuration.GetSection(nameof(ApplicationOptions.Compression)))
                .AddSingleton(x => x.GetRequiredService<IOptions<CompressionOptions>>().Value);

        /// <summary>
        /// Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services) =>
            services
                .AddResponseCompression(
                    options =>
                    {
#if (HttpsEverywhere)
                        // Enable response compression over HTTPS connections.
                        options.EnableForHttps = true;
#endif

                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var customMimeTypes = services
                            .BuildServiceProvider()
                            .GetRequiredService<CompressionOptions>()
                            .MimeTypes ?? Enumerable.Empty<string>();
                        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(customMimeTypes);
                    })
                .Configure<GzipCompressionProviderOptions>(
                    options => options.Level = CompressionLevel.Optimal);

        /// <summary>
        /// Add custom routing settings which determines how URL's are generated.
        /// </summary>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
            services.AddRouting(
                options =>
                {
                    // All generated URL's should be lower-case.
                    options.LowercaseUrls = true;
                });

#if (HttpsEverywhere)
        /// <summary>
        /// Adds the Strict-Transport-Security HTTP header to responses. This HTTP header is only relevant if you are
        /// using TLS. It ensures that content is loaded over HTTPS and refuses to connect in case of certificate
        /// errors and warnings.
        /// See https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security and
        /// http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
        /// Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
        /// Note: You can refer to the following article to clear the HSTS cache in your browser:
        /// http://classically.me/blogs/how-clear-hsts-settings-major-browsers
        /// </summary>
        public static IServiceCollection AddCustomStrictTransportSecurity(this IServiceCollection services) =>
            services
                .AddHsts(
                    options =>
                    {
                        // Preload the HSTS HTTP header for better security. See https://hstspreload.org/
#if (HstsPreload)
                        options.IncludeSubDomains = true;
                        options.MaxAge = TimeSpan.FromSeconds(31536000); // 1 Year
                        options.Preload = true;
#else
                        // options.IncludeSubDomains = true;
                        // options.MaxAge = TimeSpan.FromSeconds(31536000); // 1 Year
                        // options.Preload = true;
#endif
                    });

#endif
        public static IMvcCoreBuilder AddCustomMvcOptions(
            this IMvcCoreBuilder builder,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileOptions = builder.Services.BuildServiceProvider().GetRequiredService<CacheProfileOptions>();
                    foreach (var keyValuePair in cacheProfileOptions)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcCoreBuilder AddCustomJsonOptions(this IMvcCoreBuilder builder, IHostingEnvironment hostingEnvironment) =>
            builder.AddJsonOptions(
                options =>
                {
                    if (hostingEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        options.SerializerSettings.Formatting = Formatting.Indented;
                    }

                    // Parse dates as DateTimeOffset values by default. You should prefer using DateTimeOffset over
                    // DateTime everywhere. Not doing so can cause problems with time-zones.
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

                    // Output enumeration values as strings in JSON.
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
#if (CORS)

        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        public static IMvcCoreBuilder AddCustomCors(this IMvcCoreBuilder builder) =>
            builder.AddCors(
                options =>
                {
                    // Create named CORS policies here which you can consume using application.UseCors("PolicyName")
                    // or a [EnableCors("PolicyName")] attribute on your controller or action.
                    options.AddPolicy(
                        CorsPolicyName.AllowAny,
                        x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
                });
#endif

        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services, IHostingEnvironment hostingEnvironment) =>
            services
                // Add a way for GraphQL.NET to resolve types.
                .AddSingleton<IDependencyResolver>(x => new FuncDependencyResolver(type => x.GetRequiredService(type)))
                // Add a user context from the HttpContext and make it available in field resolvers.
                .AddGraphQLHttp<GraphQLUserContextBuilder>()
                // Add GraphQL data loader to reduce the number of calls to our repository.
                .AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>()
                .AddSingleton<DataLoaderDocumentListener>()
                // Log GraphQL request as debug messages. Turned off in production to avoid logging sensitive information.
                .AddIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x.AddSingleton<IOperationMessageListener, LogMessagesListener>());

        public static IServiceCollection AddGraphQLRelayTypes(this IServiceCollection services) =>
            services
                // Add types used for paging.
                .AddSingleton(typeof(ConnectionType<>))
                .AddSingleton(typeof(EdgeType<>))
                .AddSingleton<PageInfoType>();
    }
}
