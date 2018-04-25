namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;

    public class HumanCreatedEvent : ObjectGraphType<Human>
    {
        public HumanCreatedEvent()
        {
            this.Field<NonNullGraphType<StringGraphType>>("name");
            this.Field<StringGraphType>("homePlanet");
        }
    }
}
