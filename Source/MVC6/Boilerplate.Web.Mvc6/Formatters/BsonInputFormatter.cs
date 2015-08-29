namespace Boilerplate.Web.Mvc.Formatters
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Internal;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;

    public class BsonInputFormatter : InputFormatter
    {
        private JsonSerializerSettings _serializerSettings;

        public BsonInputFormatter()
            : this(SerializerSettingsProvider.CreateSerializerSettings())
        {
        }

        public BsonInputFormatter(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;

            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);

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

        /// <inheritdoc />
        public override Task<object> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var type = context.ModelType;
            var request = context.HttpContext.Request;

            using (var bsonReader = CreateBsonReader(context, request.Body))
            {
                bsonReader.CloseInput = false;

                var jsonSerializer = CreateJsonSerializer();

                EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> errorHandler = null;
                errorHandler = (sender, e) =>
                {
                    var exception = e.ErrorContext.Error;
                    context.ModelState.TryAddModelError(e.ErrorContext.Path, e.ErrorContext.Error);

                    // Error must always be marked as handled
                    // Failure to do so can cause the exception to be rethrown at every recursive level and
                    // overflow the stack for x64 CLR processes
                    e.ErrorContext.Handled = true;
                };
                jsonSerializer.Error += errorHandler;

                try
                {
                    return Task.FromResult(jsonSerializer.Deserialize(bsonReader, type));
                }
                finally
                {
                    // Clean up the error handler in case CreateJsonSerializer() reuses a serializer
                    if (errorHandler != null)
                    {
                        jsonSerializer.Error -= errorHandler;
                    }
                }
            }
        }

        /// <summary>
        /// Called during deserialization to get the <see cref="BsonReader"/>.
        /// </summary>
        /// <param name="context">The <see cref="InputFormatterContext"/> for the read.</param>
        /// <param name="readStream">The <see cref="Stream"/> from which to read.</param>
        /// <returns>The <see cref="BsonReader"/> used during deserialization.</returns>
        protected virtual BsonReader CreateBsonReader(
            InputFormatterContext context,
            Stream readStream)
        {
            return new BsonReader(readStream);
        }

        /// <summary>
        /// Called during deserialization to get the <see cref="JsonSerializer"/>.
        /// </summary>
        /// <returns>The <see cref="JsonSerializer"/> used during serialization and deserialization.</returns>
        protected virtual JsonSerializer CreateJsonSerializer()
        {
            return JsonSerializer.Create(SerializerSettings);
        }
    }
}