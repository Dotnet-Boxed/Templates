namespace Boilerplate.Web.Mvc.Formatters
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.Framework.OptionsModel;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class MvcBsonMvcOptionsSetup : ConfigureOptions<MvcOptions>
    {
        public MvcBsonMvcOptionsSetup(IOptions<MvcJsonOptions> jsonOptions)
            : base((_) => ConfigureMvc(_, jsonOptions.Options.SerializerSettings))
        {
        }

        public static void ConfigureMvc(MvcOptions options, JsonSerializerSettings serializerSettings)
        {
            options.OutputFormatters.Add(new JsonOutputFormatter(serializerSettings));
            options.InputFormatters.Add(new JsonInputFormatter(serializerSettings));

            options.FormatterMappings.SetMediaTypeMappingForFormat("bson", MediaTypeHeaderValue.Parse("application/bson"));

            options.ValidationExcludeFilters.Add(typeof(JToken));
        }
    }
}
