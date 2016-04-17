namespace Boilerplate.Web.Mvc
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc;

    public interface IAsyncCommand<T1, T2, T3>
    {
        Task<IActionResult> ExecuteAsync(T1 parameter1, T2 parameter2, T3 parameter3);
    }
}
