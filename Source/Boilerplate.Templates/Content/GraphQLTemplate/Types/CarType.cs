namespace ApiTemplate.Types
{
    using ApiTemplate.Models;
    using ApiTemplate.Repositories;
    using GraphQL.Types;

    public class CarType : ObjectGraphType<Car>
    {
        public CarType(IPersonRepository personRepository)
        {
            this.Field(x => x.CarId);
            // this.Field(x => x.Created);
            this.Field(x => x.Cylinders);
            this.Field(x => x.Make);
            this.Field(x => x.Model);
            // this.Field(x => x.Modified);
            this.Field<PersonType>(
                "person",
                resolve: context => personRepository.GetPerson(context.Source.OwnerId, context.CancellationToken));
        }
    }
}
