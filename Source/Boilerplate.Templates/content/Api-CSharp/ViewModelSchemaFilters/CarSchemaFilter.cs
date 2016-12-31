namespace MvcBoilerplate.ViewModelSchemaFilters
{
    using MvcBoilerplate.ViewModels;
    using Swashbuckle.Swagger.Model;
    using Swashbuckle.SwaggerGen.Generator;

    public class CarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            model.Default = new Car()
            {
                CarId = 1,
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic"
            };
        }
    }
}
