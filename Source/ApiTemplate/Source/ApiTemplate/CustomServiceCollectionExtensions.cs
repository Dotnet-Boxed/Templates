namespace ApiTemplate
{
    using System;
#if ResponseCompression
    using System.IO.Compression;
#endif
    using System.Linq;
#if Swagger
    using System.Reflection;
#endif
#if Swagger && Versioning
    using ApiTemplate.OperationFilters;
#endif
    using ApiTemplate.Options;
    using Boxed.AspNetCore;
#if Swagger
    using Boxed.AspNetCore.Swagger;
    using Boxed.AspNetCore.Swagger.OperationFilters;
    using Boxed.AspNetCore.Swagger.SchemaFilters;
#endif
#if CorrelationId
    using CorrelationId;
#endif
    using Microsoft.AspNetCore.Builder;
#if !ForwardedHeaders && HostFiltering
    using Microsoft.AspNetCore.HostFiltering;
#endif
#if Versioning
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
#endif
#if ResponseCompression
    using Microsoft.AspNetCore.ResponseCompression;
#endif
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
#if Swagger
    using Swashbuckle.AspNetCore.Swagger;
#endif

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
                // ConfigureSingleton registers IOptions<T> and also T as a singleton to the services collection.
                .ConfigureAndValidateSingleton<ApplicationOptions>(configuration)
#if ResponseCompression
                .ConfigureAndValidateSingleton<CompressionOptions>(configuration.GetSection(nameof(ApplicationOptions.Compression)))
#endif
#if ForwardedHeaders
                .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(configuration.GetSection(nameof(ApplicationOptions.ForwardedHeaders)))
#elif HostFiltering
                .ConfigureAndValidateSingleton<HostFilteringOptions>(configuration.GetSection(nameof(ApplicationOptions.HostFiltering)))
#endif
                .ConfigureAndValidateSingleton<CacheProfileOptions>(configuration.GetSection(nameof(ApplicationOptions.CacheProfiles)));

#if ResponseCompression
        /// <summary>
        /// Adds dynamic response compression to enable GZIP compression of responses. This is turned off for HTTPS
        /// requests by default to avoid the BREACH security vulnerability.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services) =>
            services
                .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
                .AddResponseCompression(
                    options =>
                    {
                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var customMimeTypes = services
                            .BuildServiceProvider()
                            .GetRequiredService<CompressionOptions>()
                            .MimeTypes ?? Enumerable.Empty<string>();
                        options.MimeTypes = customMimeTypes.Concat(ResponseCompressionDefaults.MimeTypes);

                        options.Providers.Add<BrotliCompressionProvider>();
                        options.Providers.Add<GzipCompressionProvider>();
                    });

#endif
        /// <summary>
        /// Add custom routing settings which determines how URL's are generated.
        /// </summary>
        public static IServiceCollection AddCustomRouting(this IServiceCollection services) =>
            services.AddRouting(options => options.LowercaseUrls = true);

#if HttpsEverywhere
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
#if HstsPreload
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
#if HealthCheck
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services) =>
            services
                .AddHealthChecks()
                // Add health checks for external dependencies here. See https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
                .Services;

#endif
#if Versioning
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

#endif
#if Swagger
        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllEnumsAsStrings();
                    options.DescribeAllParametersInCamelCase();
                    options.DescribeStringEnumsInCamelCase();
                    options.EnableAnnotations();

                    // Add the XML comment file for this assembly, so its contents can be displayed.
                    options.IncludeXmlCommentsIfExists(assembly);

#if Versioning
                    options.OperationFilter<ApiVersionOperationFilter>();
#endif
                    options.OperationFilter<CorrelationIdOperationFilter>();
                    options.OperationFilter<ForbiddenResponseOperationFilter>();
                    options.OperationFilter<UnauthorizedResponseOperationFilter>();

                    // Show an example model for JsonPatchDocument<T>.
                    options.SchemaFilter<JsonPatchDocumentSchemaFilter>();

#if Versioning
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new Info()
                        {
                            Title = assemblyProduct,
                            Description = apiVersionDescription.IsDeprecated ?
                                $"{assemblyDescription} This API version has been deprecated." :
                                assemblyDescription,
                            Version = apiVersionDescription.ApiVersion.ToString(),
                        };
                        options.SwaggerDoc(apiVersionDescription.GroupName, info);
                    }
#else
                    var info = new Info()
                    {
                        Title = assemblyProduct,
                        Description = assemblyDescription,
                        Version = "v1"
                    };
                    options.SwaggerDoc("v1", info);
#endif
                });

#endif
    }
}
