namespace GraphQLTemplate.Models
{
    using System;

    public class Human : Character
    {
        public DateTime DateOfBirth { get; set; }

        public string HomePlanet { get; set; }
    }
}
