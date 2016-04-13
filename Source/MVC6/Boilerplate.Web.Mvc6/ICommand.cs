namespace Boilerplate.Web.Mvc
{
    using Microsoft.AspNet.Mvc;

    public interface ICommand
    {
        IActionResult Execute();
    }
}
