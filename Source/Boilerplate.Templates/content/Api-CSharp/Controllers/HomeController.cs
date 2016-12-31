namespace MvcBoilerplate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MvcBoilerplate.Constants;

    public class HomeController : ControllerBase
    {
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index()
        {
            return this.RedirectPermanent("/swagger/ui");
        }
    }
}
