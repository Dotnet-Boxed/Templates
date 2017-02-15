namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;
#if (Swagger)
    using ApiTemplate.ViewModelSchemaFilters;
    using Swashbuckle.AspNetCore.SwaggerGen;
#endif

#if (Swagger)
    [SwaggerSchemaFilter(typeof(SaveCarSchemaFilter))]
#endif
    public class SaveCar
    {
        [Range(1, 20)]
        public int Cylinders { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
