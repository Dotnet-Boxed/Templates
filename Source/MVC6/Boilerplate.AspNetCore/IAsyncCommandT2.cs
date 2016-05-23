namespace Boilerplate.AspNetCore
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IAsyncCommand<T1, T2>
    {
        Task<IActionResult> ExecuteAsync(T1 parameter1, T2 parameter2);
    }
}
