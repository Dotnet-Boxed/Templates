namespace Boilerplate.Web.Mvc
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Mvc;

    public interface IAsyncCommand
    {
        Task<IActionResult> ExecuteAsync();
    }
}
