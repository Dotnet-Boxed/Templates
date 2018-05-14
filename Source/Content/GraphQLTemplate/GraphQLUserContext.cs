namespace GraphQLTemplate
{
    using System.Security.Claims;

    /// <summary>
    /// The GraphQL user context for the current request. The user context is accessible in field resolvers and
    /// validation rules using <c>context.UserContext.As&lt;GraphQLUserContext&gt;()</c>.
    /// </summary>
    public class GraphQLUserContext
    {
        /// <summary>
        /// Gets the current users claims principal.
        /// </summary>
        public ClaimsPrincipal User { get; set; }
    }
}
