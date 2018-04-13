namespace GraphQLTemplate.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GraphQLTemplate.Models;

    public static class Database
    {
        static Database()
        {
            Droids = new List<Droid>()
            {
                new Droid()
                {
                    Id = new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68").ToString(),
                    Name = "R2-D2",
                    Friends = new List<string>()
                    {
                        new Guid("94fbd693-2027-4804-bf40-ed427fe76fda").ToString(),
                        new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa").ToString()
                    },
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.JEDI, Episode.EMPIRE, },
                    PrimaryFunction = "Astromech"
                },
                new Droid()
                {
                    Id = new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa").ToString(),
                    Name = "C-3PO",
                    Friends = new List<string>(),
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.JEDI, Episode.EMPIRE, },
                    PrimaryFunction = "Protocol"
                }
            };
            Humans = new List<Human>()
            {
                new Human
                {
                    Id = new Guid("94fbd693-2027-4804-bf40-ed427fe76fda").ToString(),
                    Name = "Luke",
                    Friends = new List<string>()
                    {
                        new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68").ToString(),
                        new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa").ToString()
                    },
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.JEDI, Episode.EMPIRE, },
                    HomePlanet = "Tatooine"
                },
                new Human
                {
                    Id = new Guid("7f7bf389-2cfb-45f4-b91e-9d95441c1ecc").ToString(),
                    Name = "Vader",
                    Friends = new List<string>(),
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.JEDI, Episode.EMPIRE, },
                    HomePlanet = "Tatooine"
                }
            };
            Characters = Droids.AsEnumerable<Character>().Concat(Humans).ToList();
        }

        public static List<Character> Characters { get; }

        public static List<Droid> Droids { get; }

        public static List<Human> Humans { get; }
    }
}
