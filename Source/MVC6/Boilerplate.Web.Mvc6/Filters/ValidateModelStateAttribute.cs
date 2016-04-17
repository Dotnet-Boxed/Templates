namespace Boilerplate.Web.Mvc.Filters
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.AspNet.Mvc.Filters;
    using System.Linq;

    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ActionArguments.Any(x => x.Value == null))
            {
                actionContext.Result = new BadRequestObjectResult("Arguments cannot be null.");
            }
            else if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }
    }
}