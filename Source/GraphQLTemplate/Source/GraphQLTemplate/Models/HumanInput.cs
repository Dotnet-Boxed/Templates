namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public class HumanInput
    {
        public HumanInput() => this.AppearsIn = new List<Episode>();

        public string Name { get; set; } = default!;

        public DateTime DateOfBirth { get; set; }

        public string HomePlanet { get; set; } = default!;

        public List<Episode> AppearsIn { get; set; }
    }
}
