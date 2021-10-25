namespace ApiTemplate.ViewModels
{
    using System.Text.Json.Serialization;

    [JsonSerializable(typeof(Connection<Car>[]))]
    internal partial class ConnectionJsonSerializerContext : JsonSerializerContext
    {
    }
}
