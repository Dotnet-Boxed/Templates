namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;
#if Swagger
    using ApiTemplate.ViewModelSchemaFilters;
    using Swashbuckle.AspNetCore.Annotations;
#endif

#if Swagger
    /// <summary>
    /// A make and model of car.
    /// </summary>
    [SwaggerSchemaFilter(typeof(SaveCarSchemaFilter))]
#endif
    public class SaveCar
    {
#if Swagger
        /// <summary>
        /// The number of cylinders in the cars engine.
        /// </summary>
#endif
        [Range(1, 20)]
        public int Cylinders { get; set; }

#if Swagger
        /// <summary>
        /// The make of the car.
        /// </summary>
#endif
        [Required]
        public string Make { get; set; }

#if Swagger
        /// <summary>
        /// The model of the car.
        /// </summary>
#endif
        [Required]
        public string Model { get; set; }
    }
}
