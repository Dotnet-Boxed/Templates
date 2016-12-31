namespace MvcBoilerplate.ViewModelSchemaFilters
{
    using MvcBoilerplate.ViewModels;
    using Swashbuckle.Swagger.Model;
    using Swashbuckle.SwaggerGen.Generator;

    public class SaveCarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            model.Default = new SaveCar()
            {
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic"
            };
        }
    }
}
