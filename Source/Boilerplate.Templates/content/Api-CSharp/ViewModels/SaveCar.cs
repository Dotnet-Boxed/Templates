namespace MvcBoilerplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

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
