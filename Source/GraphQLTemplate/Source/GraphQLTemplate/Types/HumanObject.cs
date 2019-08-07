namespace GraphQLTemplate.Types
{
    using System.Collections.Generic;
#if Authorization
    using GraphQL.Authorization;
#endif
    using GraphQL.Types;
    using GraphQLTemplate.Constants;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;

    public class HumanObject : ObjectGraphType<Human>
    {
        public HumanObject(IHumanRepository humanRepository)
        {
            this.Name = "Human";
            this.Description = "A humanoid creature from the Star Wars universe.";
#if Authorization
            // this.AuthorizeWith(AuthorizationPolicyName.Admin); // To require authorization for all fields in this type.
#endif

            this.Field(x => x.Id, type: typeof(NonNullGraphType<IdGraphType>))
                .Description("The unique identifier of the human.");
            this.Field(x => x.Name)
                .Description("The name of the human.");
            this.Field(x => x.DateOfBirth)
#if Authorization
                .AuthorizeWith(AuthorizationPolicyName.Admin) // Require authorization to access the date of birth field.
#endif
                .Description("The humans date of birth.");
            this.Field(x => x.HomePlanet, nullable: true)
                .Description("The home planet of the human.");
            this.Field(x => x.AppearsIn, type: typeof(ListGraphType<EpisodeEnumeration>))
                .Description("Which movie they appear in.");

            this.FieldAsync<ListGraphType<CharacterInterface>, List<Character>>(
                nameof(Human.Friends),
                "The friends of the character, or an empty list if they have none.",
                resolve: context => humanRepository.GetFriends(context.Source, context.CancellationToken));

            this.Interface<CharacterInterface>();
        }
    }
}
