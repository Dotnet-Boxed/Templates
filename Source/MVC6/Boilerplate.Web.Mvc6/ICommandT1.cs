namespace Boilerplate.Web.Mvc
{
    using Microsoft.AspNet.Mvc;

    public interface ICommand<T>
    {
        IActionResult Execute(T parameter);
    }
}
