namespace GraphQLTemplate.Models
{
    using System;

    public record Human(
        Guid Id,
        string Name,
        string HomePlanet,
        DateTime DateOfBirth,
        DateTimeOffset Created,
        DateTimeOffset Modified) :
        Character(Id, Name, Created, Modified)
    {
    }
}
