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
        [HttpGet("{status}", Name = ErrorControllerRoute.GetError)]
        public ActionResult Error(string status)
        {
            // Check if I need this?
            //this.Response.StatusCode = (int)statusCode;

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