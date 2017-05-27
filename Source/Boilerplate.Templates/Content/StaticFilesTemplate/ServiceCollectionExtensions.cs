namespace StaticFilesTemplate
{
    using System.IO.Compression;
    using System.Linq;
    using StaticFilesTemplate.Constants;
    using StaticFilesTemplate.Settings;
    using Boilerplate.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ServiceCollectionExtensions
    {
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
#if (CORS)

        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        public static IServiceCollection AddCustomCors(this IServiceCollection services) =>
            services.AddCors(
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
    }
}
