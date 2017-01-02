namespace MvcBoilerplate.ViewModels
{
    using System.Collections.Generic;

    public class Page<T>
        where T : class
    {
        public int PageNumber { get; set; }

        public int Count { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
