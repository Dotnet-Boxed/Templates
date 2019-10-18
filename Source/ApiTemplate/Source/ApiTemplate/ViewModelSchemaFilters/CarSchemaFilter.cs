namespace ApiTemplate.ViewModelSchemaFilters
{
    using ApiTemplate.ViewModels;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class CarSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var car = new Car()
            {
                CarId = 1,
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic",
                Url = "/cars/1",
            };
            // model.Default = car;
            // model.Example = car;
        }
    }
}
