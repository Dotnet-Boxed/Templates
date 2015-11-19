namespace Boilerplate.Web.Mvc.Formatters
{
    using System;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.OptionsModel;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="IMvcBuilder"/> extension methods.
    /// </summary>
    public static class MvcBsonMvcBuilderExtensions
    {
        /// <summary>
        /// Adds <see cref="BsonInputFormatter"/> and <see cref="BsonOutputFormatter"/> to the input and output 
        /// formatter collections respectively.
        /// </summary>
        /// <param name="builder">The MVC builder.</param>
        public static IMvcBuilder AddBsonSerializerFormatters(this IMvcBuilder builder)
        {
            AddBsonFormatterServices(builder.Services);
            return builder;
        }

        public static IMvcCoreBuilder AddBsonFormatters(
            this IMvcCoreBuilder builder,
            Action<JsonSerializerSettings> setupAction)
        {
            AddBsonFormatterServices(builder.Services);

            if (setupAction != null)
            {
                builder.Services.Configure<MvcJsonOptions>((options) => setupAction(options.SerializerSettings));
            }

            return builder;
        }

        // Internal for testing.
        internal static void AddBsonFormatterServices(IServiceCollection services)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcBsonMvcOptionsSetup>());
        }
    }
}