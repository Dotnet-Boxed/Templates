namespace Boilerplate.Web.Mvc.Formatters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc.Formatters;
    using Microsoft.AspNet.Mvc.Internal;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    /// <summary>
    /// An output formatter that specializes in writing BSON content.
    /// </summary>
    public class BsonOutputFormatter : OutputFormatter
    {
        private JsonSerializerSettings _serializerSettings;

        public BsonOutputFormatter()
            : this(SerializerSettingsProvider.CreateSerializerSettings())
        {
        }

        public BsonOutputFormatter(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

            SupportedMediaTypes.Add(MediaTypeHeaderValues.ApplicationBson);
        }

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings"/> used to configure the <see cref="JsonSerializer"/>.
        /// </summary>
        public JsonSerializerSettings SerializerSettings
        {
            get
            {
                return _serializerSettings;
            }
            set
            {
                _serializerSettings = value;
            }
        }

        public void WriteObject(Stream stream, object value)
        {
            using (var bsonWriter = CreateBsonWriter(stream))
            {
                var jsonSerializer = CreateJsonSerializer();
                jsonSerializer.Serialize(bsonWriter, value);
            }
        }

        /// <summary>
        /// Called during serialization to create the <see cref="BsonWriter"/>.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to write to.</param>
        /// <returns>The <see cref="BsonWriter"/> used during serialization.</returns>
        protected virtual BsonWriter CreateBsonWriter(Stream stream)
        {
            var bsonWriter = new BsonWriter(stream)
            {
                CloseOutput = false
            };
            return bsonWriter;
        }

        /// <summary>
        /// Called during serialization to create the <see cref="JsonSerializer"/>.
        /// </summary>
        /// <returns>The <see cref="JsonSerializer"/> used during serialization and deserialization.</returns>
        protected virtual JsonSerializer CreateJsonSerializer()
        {
            return JsonSerializer.Create(SerializerSettings);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            WriteObject(context.HttpContext.Response.Body, context.Object);

            return Task.FromResult(true);
        }
    }
}