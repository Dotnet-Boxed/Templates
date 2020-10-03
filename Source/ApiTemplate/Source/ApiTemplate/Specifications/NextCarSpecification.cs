namespace ApiTemplate.Specifications
{
    using System;
    using ApiTemplate.Models;
    using Ardalis.Specification;

    public class NextCarSpecification : Specification<Car>
    {
        public NextCarSpecification(
            int size,
            DateTimeOffset? createdAfter)
        {
            this.Query.Skip(size);
            if (createdAfter.HasValue)
            {
                this.Query.Where(x => x.Created > createdAfter.Value);
            }
        }
    }
}
