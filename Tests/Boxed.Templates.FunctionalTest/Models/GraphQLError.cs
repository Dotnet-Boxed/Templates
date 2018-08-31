namespace Boxed.Templates.FunctionalTest.Models
{
    using System.Collections.Generic;

    public class GraphQLError
    {
        public string Message { get; set; }

        public List<GraphQLErrorLocation> Locations { get; set; }

        public GraphQLErrorExtensions Extensions { get; set; }
    }
}
