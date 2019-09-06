namespace ApiTemplate.ViewModels
{
    using System.Collections.Generic;

    public class Connection<T>
    {
        public int TotalCount { get; set; }

        public PageInfo PageInfo { get; set; }

        public List<T> Items { get; set; }
    }
}
