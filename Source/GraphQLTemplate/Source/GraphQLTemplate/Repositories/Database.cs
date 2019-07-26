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
                    ChargePeriod = new TimeSpan(5, 4, 6, 6),
                    Created = new DateTimeOffset(3001, 1, 1, 3, 5, 7, TimeSpan.Zero),
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
                    ChargePeriod = new TimeSpan(1, 5, 7, 8),
                    Created = new DateTimeOffset(3002, 2, 2, 15, 44, 32, TimeSpan.FromHours(6)),
                    Name = "C-3PO",
                    Friends = new List<Guid>(),
                    AppearsIn = new List<Episode>() { Episode.NEWHOPE, Episode.EMPIRE, Episode.JEDI, },
                    PrimaryFunction = "Protocol",
                },
                new Droid()
                {
                    Id = new Guid("bcf83480-32c3-4d79-ba5d-2bea3bd1a279"),
                    ChargePeriod = new TimeSpan(2, 4, 7, 3),
                    Created = new DateTimeOffset(3003, 3, 3, 6, 5, 47, TimeSpan.FromHours(-4)),
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
                    DateOfBirth = new DateTime(3020, 4, 5),
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
                    DateOfBirth = new DateTime(3000, 3, 1),
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
