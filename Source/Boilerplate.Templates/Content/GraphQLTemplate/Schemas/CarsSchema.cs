namespace ApiTemplate.Schemas
{
    using ApiTemplate.Queries;
    using GraphQL;
    using GraphQL.Conversion;
    using GraphQL.Types;

    public class CarsSchema : Schema
    {
        public CarsSchema(CarsQuery query, IDependencyResolver dependencyResolver)
        {
            this.DependencyResolver = dependencyResolver;
            this.Query = query;
        }
    }
}
