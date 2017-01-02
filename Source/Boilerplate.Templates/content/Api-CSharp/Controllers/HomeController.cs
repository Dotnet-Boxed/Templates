namespace ApiTemplate.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ApiTemplate.Constants;

    public class HomeController : ControllerBase
    {
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index()
        {
            return this.RedirectPermanent("/swagger/ui");
        }
    }
}
