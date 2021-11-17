namespace GraphQLTemplate.Models;

public abstract record class Character(Guid Id, string Name, DateTimeOffset Created, DateTimeOffset Modified)
{
    public List<Episode> AppearsIn { get; } = new List<Episode>();

    public List<Guid> Friends { get; } = new List<Guid>();
}
