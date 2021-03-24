namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public abstract record Character(Guid Id, string Name, DateTimeOffset Created, DateTimeOffset Modified)
    {
        public List<Episode> AppearsIn { get; } = new List<Episode>();

        public List<Guid> Friends { get; } = new List<Guid>();
    }
}
