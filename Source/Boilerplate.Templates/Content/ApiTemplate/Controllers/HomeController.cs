namespace ApiTemplate.Controllers
{
    using ApiTemplate.Constants;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Redirects to the swagger page.
        /// </summary>
        /// <returns>A 301 Moved Permanently response.</returns>
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index() => this.RedirectPermanent("/swagger");
    }
}
