namespace Boilerplate.Web.Mvc.Formatters
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.OptionsModel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    /// <summary>
    /// An action result which formats the given object as BSON.
    /// </summary>
    public class BsonResult : ActionResult
    {
        private readonly JsonSerializerSettings _serializerSettings;

        /// <summary>
        /// Creates a new <see cref="BsonResult"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to format as JSON.</param>
        public BsonResult(object value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new <see cref="BsonResult"/> with the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to format as JSON.</param>
        /// <param name="serializerSettings">The <see cref="JsonSerializerSettings"/> to be used by
        /// the formatter.</param>
        public BsonResult(object value, JsonSerializerSettings serializerSettings)
        {
            Value = value;
            _serializerSettings = serializerSettings;
        }

        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the value to be formatted.
        /// </summary>
        public object Value { get; set; }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = ContentType.Bson;

            if (StatusCode != null)
            {
                response.StatusCode = StatusCode.Value;
            }

            var serializerSettings = _serializerSettings;
            if (serializerSettings == null)
            {
                serializerSettings = context
                    .HttpContext
                    .RequestServices
                    .GetRequiredService<IOptions<MvcJsonOptions>>()
                    .Value
                    .SerializerSettings;
            }

            using (var bsonWriter = new BsonWriter(response.Body))
            {
                bsonWriter.CloseOutput = false;
                var jsonSerializer = JsonSerializer.Create(serializerSettings);
                jsonSerializer.Serialize(bsonWriter, Value);
            }

            return Task.FromResult(true);
        }
    }
}