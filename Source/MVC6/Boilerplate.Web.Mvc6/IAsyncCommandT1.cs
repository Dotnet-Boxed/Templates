namespace Boilerplate.AspNetCore
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IAsyncCommand<T>
    {
        Task<IActionResult> ExecuteAsync(T parameter);
    }
}
