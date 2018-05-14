namespace GraphQLTemplate
{
    using System.Threading.Tasks;
    using GraphQL.Server.Transports.AspNetCore;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Builds a GraphQL user context from a <see cref="HttpContext"/>.
    /// </summary>
    public class GraphQLUserContextBuilder : IUserContextBuilder
    {
        public Task<object> BuildUserContext(HttpContext httpContext) =>
            Task.FromResult<object>(new GraphQLUserContext() { User = httpContext.User });
    }
}
