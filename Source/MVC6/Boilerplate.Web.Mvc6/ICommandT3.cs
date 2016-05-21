namespace Boilerplate.AspNetCore
{
    using Microsoft.AspNetCore.Mvc;

    public interface ICommand<T1, T2, T3>
    {
        IActionResult Execute(T1 parameter1, T2 parameter2, T3 parameter3);
    }
}
