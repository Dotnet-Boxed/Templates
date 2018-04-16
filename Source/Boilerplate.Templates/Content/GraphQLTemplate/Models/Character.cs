namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class Character
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Guid> Friends { get; set; }

        public List<Episode> AppearsIn { get; set; }
    }
}
