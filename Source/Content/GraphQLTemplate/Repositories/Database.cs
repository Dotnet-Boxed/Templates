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
                    Id = new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68"),
                    Created = new DateTime(3001, 1, 1),
                    Name = "R2-D2",
                    Friends = new List<Guid>()
                    {
                        new Guid("94fbd693-2027-4804-bf40-ed427fe76fda"),
                        new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa"),
                    },
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.EMPIRE, Episode.JEDI, },
                    PrimaryFunction = "Astromech",
                },
                new Droid()
                {
                    Id = new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa"),
                    Created = new DateTime(3002, 2, 2),
                    Name = "C-3PO",
                    Friends = new List<Guid>(),
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.EMPIRE, Episode.JEDI, },
                    PrimaryFunction = "Protocol",
                },
                new Droid()
                {
                    Id = new Guid("bcf83480-32c3-4d79-ba5d-2bea3bd1a279"),
                    Created = new DateTime(3003, 3, 3),
                    Name = "2-1B",
                    Friends = new List<Guid>(),
                    AppearsIn = new List<Episode>() { Episode.EMPIRE, },
                    PrimaryFunction = "Surgical",
                },
            };
            Humans = new List<Human>()
            {
                new Human()
                {
                    Id = new Guid("94fbd693-2027-4804-bf40-ed427fe76fda"),
                    Name = "Luke Skywalker",
                    Friends = new List<Guid>()
                    {
                        new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68"),
                        new Guid("c2bbf949-764b-4d4f-bce6-0404211810fa"),
                    },
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.EMPIRE, Episode.JEDI, },
                    HomePlanet = "Tatooine",
                },
                new Human()
                {
                    Id = new Guid("7f7bf389-2cfb-45f4-b91e-9d95441c1ecc"),
                    Name = "Darth Vader",
                    Friends = new List<Guid>(),
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.EMPIRE, Episode.JEDI, },
                    HomePlanet = "Tatooine",
                },
            };
            Characters = Droids.AsEnumerable<Character>().Concat(Humans).ToList();
        }

        public static List<Character> Characters { get; }

        public static List<Droid> Droids { get; }

        public static List<Human> Humans { get; }
    }
}
