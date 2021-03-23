namespace GraphQLTemplate.Schemas
{
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Resolvers;
    using GraphQLTemplate.Types;
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
    public class QueryObject : ObjectType<QueryResolvers>
    {
        public QueryObject()
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";
        }

        protected override void Configure(IObjectTypeDescriptor<QueryResolvers> descriptor)
        {
            descriptor
                .Field(x => x.GetDroidsAsync(default!))
                .Description("Gets droids.")
                .UsePaging<DroidObject, Droid>()
                .UseFiltering()
                .UseSorting();
            descriptor
                .Field(x => x.GetHumansAsync(default!))
                .Description("Gets humans.")
                .UsePaging<HumanObject, Human>()
                .UseFiltering()
                .UseSorting();
        }
    }
}
