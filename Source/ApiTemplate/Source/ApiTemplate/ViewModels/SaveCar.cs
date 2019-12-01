namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

#if Swagger
    /// <summary>
    /// A make and model of car.
    /// </summary>
#endif
    public class SaveCar
    {
#if Swagger
        /// <summary>
        /// The number of cylinders in the cars engine.
        /// </summary>
        /// <example>6</example>
#endif
        [Range(1, 20)]
        public int Cylinders { get; set; }

#if Swagger
        /// <summary>
        /// The make of the car.
        /// </summary>
        /// <example>Honda</example>
#endif
        [Required]
        public string Make { get; set; }

#if Swagger
        /// <summary>
        /// The model of the car.
        /// </summary>
        /// <example>Civic</example>
#endif
        [Required]
        public string Model { get; set; }
    }
}
