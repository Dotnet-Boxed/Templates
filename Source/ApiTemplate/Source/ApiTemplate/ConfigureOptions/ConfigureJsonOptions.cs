namespace ApiTemplate.ConfigureOptions;

#if Controllers
using ApiTemplate.ViewModels;
#endif
using Microsoft.AspNetCore.Hosting;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public ConfigureJsonOptions(IWebHostEnvironment webHostEnvironment) =>
        this.webHostEnvironment = webHostEnvironment;

    public void Configure(JsonOptions options)
    {
        var jsonSerializerOptions = options.JsonSerializerOptions;
        jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;

        // Pretty print the JSON in development for easier debugging.
        jsonSerializerOptions.WriteIndented = this.webHostEnvironment.IsDevelopment() ||
            this.webHostEnvironment.IsEnvironment(Constants.EnvironmentName.Test);
#if Controllers

        jsonSerializerOptions.AddContext<CustomJsonSerializerContext>();
#endif
    }
}
