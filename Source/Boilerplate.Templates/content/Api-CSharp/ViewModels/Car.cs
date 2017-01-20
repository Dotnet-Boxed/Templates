namespace ApiTemplate.ViewModels
{
#if (Swagger)
    using ApiTemplate.ViewModelSchemaFilters;
    using Swashbuckle.SwaggerGen.Annotations;

    [SwaggerSchemaFilter(typeof(CarSchemaFilter))]
#endif
    public class Car
    {
        public int CarId { get; set; }

        public int Cylinders { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}
