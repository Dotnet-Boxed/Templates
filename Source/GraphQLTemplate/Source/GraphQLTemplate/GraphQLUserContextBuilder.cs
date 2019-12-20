namespace GraphQLTemplate
{
    using System;
    using System.Threading.Tasks;
    using GraphQL.Server.Transports.AspNetCore;
    using Microsoft.AspNetCore.Http;

    public class GraphQLUserContextBuilder : IUserContextBuilder
    {
        public Task<object> BuildUserContext(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            return Task.FromResult<object>(new GraphQLUserContext() { User = httpContext.User });
        }
    }
}
