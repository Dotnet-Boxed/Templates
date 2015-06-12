namespace MvcBoilerplate.Controllers
{
    using Microsoft.AspNet.Mvc;
    using MvcBoilerplate.Constants;

    public class HomeController : Controller
    {
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index()
        {
            return this.View(HomeControllerAction.Index);
        }

        [HttpGet("about", Name = HomeControllerRoute.GetAbout)]
        public IActionResult About()
        {
            return this.View(HomeControllerAction.About);
        }

        [HttpGet("contact", Name = HomeControllerRoute.GetContact)]
        public IActionResult Contact()
        {
            return this.View(HomeControllerAction.Contact);
        }
    }
}
