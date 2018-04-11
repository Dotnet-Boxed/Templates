namespace ApiTemplate.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Models;

    public interface IPersonRepository
    {
        Task<Person> GetPerson(int personId, CancellationToken cancellationToken);
    }
}
