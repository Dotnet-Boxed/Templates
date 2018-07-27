namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;

    public class HumanInputObject : InputObjectGraphType<Human>
    {
        public HumanInputObject()
        {
            this.Name = "HumanInput";
            this.Description = "A humanoid creature from the Star Wars universe.";

            this.Field(x => x.Name)
                .Description("The name of the human.");
            this.Field(x => x.DateOfBirth)
                .Description("The humans date of birth.");
            this.Field(x => x.HomePlanet, nullable: true)
                .Description("The home planet of the human.");
            this.Field(x => x.AppearsIn, type: typeof(ListGraphType<EpisodeEnumeration>))
                .Description("Which movie they appear in.");
        }
    }
}
