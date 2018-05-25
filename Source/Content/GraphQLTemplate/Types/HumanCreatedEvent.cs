namespace GraphQLTemplate.Types
{
    using GraphQLTemplate.Repositories;

    public class HumanCreatedEvent : HumanObject
    {
        public HumanCreatedEvent(IHumanRepository humanRepository)
            : base(humanRepository)
        {
        }
    }
}
