namespace ApiTemplate.Models
{
    using System;

    public class Car : IModifiableResource
    {
        public int CarId { get; set; }

        public DateTimeOffset Created { get; set; }

        public int Cylinders { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public DateTimeOffset Modified { get; set; }

        string IModifiableResource.ETag => this.GetWeakETag(this.CarId.ToString());

        DateTimeOffset? IModifiableResource.LastModified => this.Modified;
    }
}