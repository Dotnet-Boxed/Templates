namespace ApiTemplate.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public class PersonRepository : IPersonRepository
    {
        private static readonly List<Person> People;

        static PersonRepository()
        {
            People = new List<Person>()
            {
                new Person()
                {
                    PersonId = 1,
                    Gender = Gender.Female,
                    Name = "Muhammad",
                },
                new Person()
                {
                    PersonId = 2,
                    Gender = Gender.Female,
                    Name = "Rehan",
                },
                new Person()
                {
                    PersonId = 3,
                    Gender = Gender.Female,
                    Name = "Saeed",
                }
            };
        }

        public Task<Person> GetPerson(int personId, CancellationToken cancellationToken) =>
            Task.FromResult(People.FirstOrDefault(x => x.PersonId == personId));
    }
}
