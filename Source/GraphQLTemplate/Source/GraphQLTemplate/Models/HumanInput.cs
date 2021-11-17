namespace GraphQLTemplate.Models;

public record class HumanInput(string Name, string HomePlanet, DateTime DateOfBirth, List<Episode>? AppearsIn)
{
}
