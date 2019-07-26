namespace GraphQLTemplate.Types
{
    using GraphQL.Types;
    using GraphQLTemplate.Models;

    public class EpisodeEnumeration : EnumerationGraphType<Episode>
    {
        public EpisodeEnumeration()
        {
            this.Name = "Episode";
            this.Description = "One of the films in the Star Wars Trilogy.";

            this.AddValue("NEWHOPE", "Released in 1977.", 4);
            this.AddValue("EMPIRE", "Released in 1980.", 5);
            this.AddValue("JEDI", "Released in 1983.", 6);
        }
    }
}
