namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class Character
    {
        public Character()
        {
            this.AppearsIn = new List<Episode>();
            this.Friends = new List<Guid>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public List<Guid> Friends { get; set; }

        public List<Episode> AppearsIn { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
