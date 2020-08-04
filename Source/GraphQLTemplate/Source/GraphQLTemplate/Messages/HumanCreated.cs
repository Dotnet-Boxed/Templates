namespace GraphQLTemplate.Messages
{
    using System.Collections.Generic;
    using System.Threading;
    using GraphQLTemplate.Models;
    using HotChocolate.Subscriptions;

    public class HumanCreatedMessage : IEventStream<Human>
    {

        public IAsyncEnumerator<Human> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
