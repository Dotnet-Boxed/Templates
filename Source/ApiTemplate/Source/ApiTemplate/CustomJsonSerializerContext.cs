namespace ApiTemplate
{
    using System.Text.Json.Serialization;
    using ApiTemplate.ViewModels;

    [JsonSerializable(typeof(Car[]))]
    [JsonSerializable(typeof(Connection<Car>[]))]
    [JsonSerializable(typeof(SaveCar[]))]
    public partial class CustomJsonSerializerContext : JsonSerializerContext
    {
    }
}
