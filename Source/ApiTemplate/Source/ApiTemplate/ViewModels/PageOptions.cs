namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PageOptions
    {
        [Range(1, int.MaxValue)]
        public int? Page { get; set; } = 1;

        [Range(1, 20)]
        public int? Count { get; set; } = 10;
    }
}
