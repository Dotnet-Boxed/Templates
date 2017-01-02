namespace ApiTemplate
{
    using System.Threading.Tasks;

    public interface IConnectionTester
    {
        Task TestConnection();
    }
}
