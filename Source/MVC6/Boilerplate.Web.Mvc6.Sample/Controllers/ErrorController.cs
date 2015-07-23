namespace MvcBoilerplate.Controllers
{
    using Boilerplate.Web.Mvc;
    using Microsoft.AspNet.Mvc;
    using MvcBoilerplate.Constants;

    /// <summary>
    /// Provides methods that respond to HTTP requests with HTTP errors.
    /// </summary>
    [Route("[controller]")]
    public sealed class ErrorController : Controller
    {
        #region Public Methods

        // [OutputCache(CacheProfile = CacheProfileName.Error)]
        [HttpGet("{statusCode}/{status}", Name = ErrorControllerRoute.GetError)]
        public ActionResult Error(int statusCode, string status)
        {
            this.Response.StatusCode = statusCode;

            ActionResult result;
            if (this.Request.IsAjaxRequest())
            {
                // This allows us to show errors even in partial views.
                result = this.PartialView(ErrorControllerAction.Error);
            }
            else
            {
                result = this.View(ErrorControllerAction.Error);
            }

            return result;
        }

        #endregion
    }
}