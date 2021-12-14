namespace GraphQLTemplate.Models;

public record class Human(
    Guid Id,
    string Name,
    string HomePlanet,
    DateOnly DateOfBirth,
    DateTimeOffset Created,
    DateTimeOffset Modified) :
    Character(Id, Name, Created, Modified)
{
}
