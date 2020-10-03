namespace ApiTemplate.Specifications
{
    using System;
    using ApiTemplate.Models;
    using Ardalis.Specification;

    public class PreviousCarSpecification : Specification<Car>
    {
        public PreviousCarSpecification(
            int size,
            DateTimeOffset? createdBefore)
        {
            this.Query.Take(size);
            if (createdBefore.HasValue)
            {
                this.Query.Where(x => x.Created < createdBefore.Value);
            }
        }
    }
}
