namespace GraphQLTemplate.IntegrationTest.Models
{
    using System.Collections.Generic;

    public class GraphQLError
    {
        public GraphQLError() => this.Locations = new List<GraphQLErrorLocation>();

        public string Message { get; set; }

        public List<GraphQLErrorLocation> Locations { get; }

        public GraphQLErrorExtensions Extensions { get; set; }
    }
}
