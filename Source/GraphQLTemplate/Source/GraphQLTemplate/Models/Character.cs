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

        public string Name { get; set; }

        public List<Guid> Friends { get; }

        public List<Episode> AppearsIn { get; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
