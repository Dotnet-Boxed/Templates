namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public abstract class Character
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Friends { get; set; }

        public List<Episode> AppearsIn { get; set; }
    }
}
