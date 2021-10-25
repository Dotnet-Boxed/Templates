namespace ApiTemplate.ViewModels
{
    using System.Text.Json.Serialization;

    [JsonSerializable(typeof(Car[]))]
    internal partial class CarJsonSerializerContext : JsonSerializerContext
    {
    }
}
