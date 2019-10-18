namespace ApiTemplate.ViewModelSchemaFilters
{
    using ApiTemplate.ViewModels;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SaveCarSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic",
            };
            // model.Default = saveCar;
            // model.Example = saveCar;
        }
    }
}
