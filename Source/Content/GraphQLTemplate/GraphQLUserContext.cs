namespace GraphQLTemplate
{
    using System.Security.Claims;

    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
