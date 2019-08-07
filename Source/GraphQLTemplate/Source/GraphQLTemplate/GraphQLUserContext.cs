namespace GraphQLTemplate
{
    using System.Security.Claims;
#if Authorization
    using GraphQL.Authorization;
#endif

    /// <summary>
    /// The GraphQL user context for the current request. The user context is accessible in field resolvers and
    /// validation rules using <c>context.UserContext.As&lt;GraphQLUserContext&gt;()</c>.
    /// </summary>
#if Authorization
    public class GraphQLUserContext : IProvideClaimsPrincipal
#else
    public class GraphQLUserContext
#endif
    {
        /// <summary>
        /// Gets the current users claims principal.
        /// </summary>
        public ClaimsPrincipal User { get; set; }
    }
}
