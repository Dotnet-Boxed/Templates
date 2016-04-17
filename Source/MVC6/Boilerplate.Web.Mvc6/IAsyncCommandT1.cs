namespace Boilerplate.Web.Mvc
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc;

    public interface IAsyncCommand<T>
    {
        Task<IActionResult> ExecuteAsync(T parameter);
    }
}
