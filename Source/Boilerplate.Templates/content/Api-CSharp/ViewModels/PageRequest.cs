namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PageRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(3, 20)]
        public int Count { get; set; } = 10;
    }
}
