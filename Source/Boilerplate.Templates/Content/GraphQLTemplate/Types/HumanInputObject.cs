namespace GraphQLTemplate.Types
{
    using GraphQL.Types;

    public class HumanInputObject : InputObjectGraphType
    {
        public HumanInputObject()
        {
            this.Name = "HumanInput";
            this.Description = "A humanoid creature from the Star Wars universe.";

            this.Field<NonNullGraphType<StringGraphType>>("name");
            this.Field<StringGraphType>("homePlanet");
        }
    }
}
