namespace ApiTemplate.Types
{
    using ApiTemplate.Models;
    using GraphQL.Types;

    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            // this.Field(x => x.Gender);
            this.Field(x => x.Name);
            this.Field(x => x.PersonId);
        }
    }
}
