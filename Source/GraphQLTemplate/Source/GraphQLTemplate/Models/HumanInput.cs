namespace GraphQLTemplate.Models;

public record class HumanInput(string Name, string HomePlanet, DateOnly DateOfBirth, List<Episode>? AppearsIn)
{
}
