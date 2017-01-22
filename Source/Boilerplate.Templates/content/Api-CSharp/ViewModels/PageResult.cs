namespace ApiTemplate.ViewModels
{
    using System.Collections.Generic;

    public class PageResult<T>
        where T : class
    {
        public int Page { get; set; }

        public int Count { get; set; }

        public int Total { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
