namespace GraphQLTemplate.Models;

public record class Droid(
    Guid Id,
    string Name,
    string PrimaryFunction,
    TimeSpan ChargePeriod,
    DateTimeOffset Manufactured,
    DateTimeOffset Created,
    DateTimeOffset Modified) :
    Character(Id, Name, Created, Modified)
{
}
