namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    public interface ICommand<T>
    {
        IActionResult Execute(T parameter);
    }
}
