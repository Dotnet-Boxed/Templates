namespace GraphQLTemplate.Models
{
    using System;

    public record Droid(
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
}
