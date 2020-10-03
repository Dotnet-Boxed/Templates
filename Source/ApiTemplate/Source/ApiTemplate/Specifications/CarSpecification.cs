namespace ApiTemplate.Specifications
{
    using System;
    using ApiTemplate.Models;
    using Ardalis.Specification;

    public class CarSpecification : Specification<Car>
    {
        public CarSpecification(
            int? first,
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore)
        {
            if (createdAfter.HasValue)
            {
                this.Query.Where(x => x.Created > createdAfter.Value);
            }

            if (createdBefore.HasValue)
            {
                this.Query.Where(x => x.Created < createdBefore.Value);
            }

            if (first.HasValue)
            {
                this.Query.Take(first.Value);
            }

            if (last.HasValue)
            {
                this.Query.Take(last.Value).OrderByDescending(x => x.Created);
            }
        }
    }
}
