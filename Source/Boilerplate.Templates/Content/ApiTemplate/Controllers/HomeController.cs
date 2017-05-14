namespace ApiTemplate.Controllers
{
    using ApiTemplate.Constants;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index() => this.RedirectPermanent("/swagger");
    }
}
