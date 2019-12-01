namespace ApiTemplate.ViewModels
{
#if Swagger
    /// <summary>
    /// A make and model of car.
    /// </summary>
#endif
    public class Car
    {
#if Swagger
        /// <summary>
        /// The cars unique identifier.
        /// </summary>
        /// <example>1</example>
#endif
        public int CarId { get; set; }

#if Swagger
        /// <summary>
        /// The number of cylinders in the cars engine.
        /// </summary>
        /// <example>6</example>
#endif
        public int Cylinders { get; set; }

#if Swagger
        /// <summary>
        /// The make of the car.
        /// </summary>
        /// <example>Honda</example>
#endif
        public string Make { get; set; }

#if Swagger
        /// <summary>
        /// The model of the car.
        /// </summary>
        /// <example>Civic</example>
#endif
        public string Model { get; set; }

#if Swagger
        /// <summary>
        /// The URL used to retrieve the resource conforming to REST'ful JSON http://restfuljson.org/.
        /// </summary>
        /// <example>/cars/1</example>
#endif
        public string Url { get; set; }
    }
}
