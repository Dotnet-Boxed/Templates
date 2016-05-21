namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    public interface ICommand
    {
        IActionResult Execute();
    }
}
