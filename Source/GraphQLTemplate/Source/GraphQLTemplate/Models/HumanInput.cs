namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public record HumanInput(string Name, string HomePlanet, DateTime DateOfBirth, List<Episode>? AppearsIn)
    {
    }
}
