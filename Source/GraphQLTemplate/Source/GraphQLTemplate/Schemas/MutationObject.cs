namespace GraphQLTemplate.Schemas
{
    using GraphQLTemplate.Resolvers;
    using HotChocolate.Types;

    /// <summary>
    /// All mutations defined in the schema used to modify data.
    /// </summary>
    /// <example>
    /// This is an example mutation to create a new human.
    /// <c>
    /// mutation createHuman($human: HumanInput!) {
    ///   createHuman(human: $human)
    ///   {
    ///     id
    ///     name
    ///     dateOfBirth
    ///     appearsIn
    ///   }
    /// }
    /// This is an example JSON of the variables you also need to specify to create a new human:
    /// {
    ///  "human": {
    ///     "name": "Muhammad Rehan Saeed",
    ///     "homePlanet": "Earth",
    ///     "dateOfBirth": "2000-01-01",
    ///     "appearsIn": [ "NEWHOPE" ]
    ///   }
    /// }
    /// </c>
    /// These can be customized by the client.
    /// </example>
    public class MutationObject : ObjectType<MutationResolvers>
    {
        protected override void Configure(IObjectTypeDescriptor<MutationResolvers> descriptor)
        {
            descriptor
                .Name("Mutation")
                .Description("The mutation type, represents all updates we can make to our data.");

            descriptor
                .Field(x => x.CreateHumanAsync(default!, default!))
                .Description("Create a new human.")
                .Argument("human", x => x.Description("The human you want to create."));
        }
    }
}
