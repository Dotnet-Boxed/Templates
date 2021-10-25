namespace ApiTemplate.ViewModels
{
    using System.Text.Json.Serialization;

    [JsonSerializable(typeof(SaveCar[]))]
    internal partial class SaveCarJsonSerializerContext : JsonSerializerContext
    {
    }
}
