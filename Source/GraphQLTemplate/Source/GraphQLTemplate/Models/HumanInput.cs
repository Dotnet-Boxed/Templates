namespace GraphQLTemplate.Models
{
    using System;
    using System.Collections.Generic;

    public record class HumanInput(string Name, string HomePlanet, DateOnly DateOfBirth, List<Episode>? AppearsIn)
    {
    }
}
