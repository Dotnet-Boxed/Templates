namespace ApiTemplate.Queries
{
    using ApiTemplate.Repositories;
    using ApiTemplate.Types;
    using GraphQL.Types;

    public class CarsQuery : ObjectGraphType<object>
    {
        public CarsQuery(ICarRepository carRepository)
        {
            this.Name = "Query";
            // this.Description = null;
            this.Field<ListGraphType<CarType>>(
                "cars",
                resolve: context => carRepository.GetPage(1, 10, context.CancellationToken));
        }
    }
}
