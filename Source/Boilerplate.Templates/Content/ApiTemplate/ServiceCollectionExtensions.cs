namespace ApiTemplate
{
    using System;
    using System.IO.Compression;
    using System.Linq;
#if (Swagger)
    using System.Reflection;
#endif
    using ApiTemplate.Commands;
#if (CORS)
    using ApiTemplate.Constants;
#endif
#if (Versioning)
    using ApiTemplate.OperationFilters;
#endif
    using ApiTemplate.Repositories;
    using ApiTemplate.Services;
    using ApiTemplate.Settings;
    using ApiTemplate.Translators;
    using ApiTemplate.ViewModels;
    using Boilerplate;
    using Boilerplate.AspNetCore;
    using Boilerplate.AspNetCore.Filters;
#if (Swagger)
    using Boilerplate.AspNetCore.Swagger;
    using Boilerplate.AspNetCore.Swagger.OperationFilters;
    using Boilerplate.AspNetCore.Swagger.SchemaFilters;
#endif
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
#if (Versioning)
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
#endif
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
#if (Swagger)
    using Swashbuckle.AspNetCore.Swagger;
#endif

    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures caching for the application. Registers the <see cref="IDistributedCache"/> and
        /// <see cref="IMemoryCache"/> types with the services collection or IoC container. The
        /// <see cref="IDistributedCache"/> is intended to be used in cloud hosted scenarios where there is a shared
        /// cache, which is shared between multiple instances of the application. Use the <see cref="IMemoryCache"/>
        /// otherwise.
        /// </summary>
        public static IServiceCollection AddCaching(this IServiceCollection services) =>
            services
                // Adds IMemoryCache which is a simple in-memory cache.
                .AddMemoryCache()
                // Adds IDistributedCache which is a distributed cache shared between multiple servers. This adds a
                // default implementation of IDistributedCache which is not distributed. See below:
                .AddDistributedMemoryCache();
                // Uncomment the following line to use the Redis implementation of IDistributedCache. This will
                // override any previously registered IDistributedCache service.
                // Redis is a very fast cache provider and the recommended distributed cache provider.
                // .AddDistributedRedisCache(
                //     options =>
                //     {
                //     });
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
                // Adds IOptions<CacheProfileSettings> to the services container.
                .Configure<CacheProfileSettings>(configuration.GetSection(nameof(CacheProfileSettings)));

        /// <summary>
        /// Adds response compression to enable GZIP compression of responses.
        /// </summary>
        public static IServiceCollection AddCustomResponseCompression(
            this IServiceCollection services,
            IConfigurationRoot configuration) =>
            services
                .AddResponseCompression(
                    options =>
                    {
#if (HttpsEverywhere)
                        // Enable response compression over HTTPS connections.
                        options.EnableForHttps = true;
#endif
                        // Add additional MIME types (other than the built in defaults) to enable GZIP compression for.
                        var responseCompressionSettings = configuration.GetSection<ResponseCompressionSettings>(
                            nameof(ResponseCompressionSettings));
                        options.MimeTypes = ResponseCompressionDefaults
                            .MimeTypes
                            .Concat(responseCompressionSettings.MimeTypes);
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
                    // Improve SEO by stopping duplicate URL's due to case differences or trailing slashes.
                    // See http://googlewebmastercentral.blogspot.co.uk/2010/04/to-slash-or-not-to-slash.html
                    // All generated URL's should append a trailing slash.
                    options.AppendTrailingSlash = true;
                    // All generated URL's should be lower-case.
                    options.LowercaseUrls = true;
                });

#if (Versioning)
        public static IServiceCollection AddCustomVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;
                });

#endif
        public static IMvcCoreBuilder AddCustomMvcOptions(
            this IMvcCoreBuilder builder,
            IConfigurationRoot configuration,
            IHostingEnvironment hostingEnvironment) =>
            builder.AddMvcOptions(
                options =>
                {
                    // Controls how controller actions cache content from the appsettings.json file.
                    var cacheProfileSettings = configuration.GetSection<CacheProfileSettings>();
                    foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
                    {
                        options.CacheProfiles.Add(keyValuePair);
                    }

                    if (hostingEnvironment.IsDevelopment())
                    {
                        // Lets you pass a format parameter into the query string to set the response type:
                        // e.g. ?format=application/json. Good for debugging.
                        options.Filters.Add(new FormatFilterAttribute());
                    }

                    // Check model state for null or invalid models and automatically return a 400 Bad Request.
                    options.Filters.Add(new ValidateModelStateAttribute());

                    // Remove string and stream output formatters. These are not useful for an API serving JSON or XML.
                    options.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    options.OutputFormatters.RemoveType<StringOutputFormatter>();

                    // Returns a 406 Not Acceptable if the MIME type in the Accept HTTP header is not valid.
                    options.ReturnHttpNotAcceptable = true;
                });

        /// <summary>
        /// Adds customized JSON serializer settings.
        /// </summary>
        public static IMvcCoreBuilder AddCustomJsonOptions(this IMvcCoreBuilder builder) =>
            builder.AddJsonOptions(
                options =>
                {
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
#if (Swagger)
        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(
                options =>
                {
                    var assembly = typeof(Startup).GetTypeInfo().Assembly;
                    var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                    var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

                    options.DescribeAllEnumsAsStrings();
                    options.DescribeAllParametersInCamelCase();
                    options.DescribeStringEnumsInCamelCase();

                    // Add the XML comment file for this assembly, so it's contents can be displayed.
                    options.IncludeXmlCommentsIfExists(assembly);

#if (Versioning)
                    options.OperationFilter<ApiVersionOperationFilter>();
#endif
#if (RequestId)
                    // Show a text-box to edit the X-Request-ID HTTP header.
                    options.OperationFilter<RequestIdOperationFilter>();
#endif
#if (UserAgent)
                    // Show a text-box to edit the User-Agent HTTP header.
                    options.OperationFilter<UserAgentOperationFilter>();
#endif
                    // Show an example model for JsonPatchDocument<T>.
                    options.SchemaFilter<JsonPatchDocumentSchemaFilter>();
                    // Show an example model for ModelStateDictionary.
                    options.SchemaFilter<ModelStateDictionarySchemaFilter>();

#if (Versioning)
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        var info = new Info()
                        {
                            Title = $"{assemblyProduct} (Version {apiVersionDescription.ApiVersion})",
                            Description = apiVersionDescription.IsDeprecated ?
                                $"{assemblyDescription} This API version has been deprecated." :
                                assemblyDescription,
                            Version = apiVersionDescription.ApiVersion.ToString()
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
        /// <summary>
        /// Adds project commands.
        /// </summary>
        /// <remarks>
        /// AddSingleton - Only one instance is ever created and returned.
        /// AddScoped - A new instance is created and returned for each request/response cycle.
        /// AddTransient - A new instance is created and returned each time.
        /// </remarks>
        public static IServiceCollection AddCommands(this IServiceCollection services) =>
            services
                .AddScoped<IDeleteCarCommand, DeleteCarCommand>()
                .AddScoped(x => new Lazy<IDeleteCarCommand>(() => x.GetRequiredService<IDeleteCarCommand>()))
                .AddScoped<IGetCarCommand, GetCarCommand>()
                .AddScoped(x => new Lazy<IGetCarCommand>(() => x.GetRequiredService<IGetCarCommand>()))
                .AddScoped<IGetCarPageCommand, GetCarPageCommand>()
                .AddScoped(x => new Lazy<IGetCarPageCommand>(() => x.GetRequiredService<IGetCarPageCommand>()))
                .AddScoped<IPatchCarCommand, PatchCarCommand>()
                .AddScoped(x => new Lazy<IPatchCarCommand>(() => x.GetRequiredService<IPatchCarCommand>()))
                .AddScoped<IPostCarCommand, PostCarCommand>()
                .AddScoped(x => new Lazy<IPostCarCommand>(() => x.GetRequiredService<IPostCarCommand>()))
                .AddScoped<IPutCarCommand, PutCarCommand>()
                .AddScoped(x => new Lazy<IPutCarCommand>(() => x.GetRequiredService<IPutCarCommand>()));

        /// <summary>
        /// Adds project repositories.
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ICarRepository, CarRepository>();

        /// <summary>
        /// Adds project translators.
        /// </summary>
        public static IServiceCollection AddTranslators(this IServiceCollection services) =>
            services
                .AddSingleton<ITranslator<Models.Car, Car>, CarToCarTranslator>()
                .AddSingleton<ITranslator<Models.Car, SaveCar>, CarToSaveCarTranslator>()
                .AddSingleton<ITranslator<SaveCar, Models.Car>, CarToSaveCarTranslator>();

        /// <summary>
        /// Adds project services.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
