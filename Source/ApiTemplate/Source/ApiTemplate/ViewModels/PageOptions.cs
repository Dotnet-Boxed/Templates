namespace ApiTemplate.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PageOptions
    {
        [Range(1, 20)]
        public int? First { get; set; }

        [Range(1, 20)]
        public int? Last { get; set; }

        public string After { get; set; }

        public string Before { get; set; }
    }
}
