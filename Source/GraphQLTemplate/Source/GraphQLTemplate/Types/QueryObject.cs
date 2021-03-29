namespace GraphQLTemplate.Types
{
    using GraphQLTemplate.Resolvers;
    using HotChocolate.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>
    /// <example>
    /// The is an example query to get a human and the details of their friends.
    /// <c>
    /// query getHuman {
    ///   node(id: "SHVtYW4KZzk0ZmJkNjkzMjAyNzQ4MDRiZjQwZWQ0MjdmZTc2ZmRh") {
    ///     id
    ///     ... on Human {
    ///       name
    ///       dateOfBirth
    ///       homePlanet
    ///       appearsIn
    ///       friends {
    ///         id
    ///         name
    ///         ... on Droid {
    ///           primaryFunction
    ///           manufactured
    ///           chargePeriod
    ///         }
    ///         ... on Human {
    ///           dateOfBirth
    ///           homePlanet
    ///         }
    ///       }
    ///       created
    ///       modified
    ///     }
    ///   }
    /// }
    /// </c>
    /// It retrieves common properties, as well as properties specific to droids and humans.
    /// </example>
    public class QueryObject : ObjectType<QueryResolver>
    {
        protected override void Configure(IObjectTypeDescriptor<QueryResolver> descriptor)
        {
            descriptor
                .Name("Query")
                .Description("The query type, represents all of the entry points into our object graph.");

            descriptor
                .Field(x => x.GetDroidsAsync(default!, default!))
                .Description("Gets droids.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();
            descriptor
                .Field(x => x.GetDroidByIdsAsync(default!, default!, default!))
                .Description("Gets droids by one or more unique identifiers.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();

            descriptor
                .Field(x => x.GetHumansAsync(default!, default!))
                .Description("Gets humans.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();
            descriptor
                .Field(x => x.GetHumansByIdsAsync(default!, default!, default!))
                .Description("Gets humans by one or more unique identifiers.")
                .UsePaging()
                .UseFiltering()
                .UseSorting();
        }
    }
}
