namespace Boilerplate.AspNetCore.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Validates the action's arguments and model state. If any of the arguments are <c>null</c> or the model state is
    /// invalid, returns a 400 Bad Request result.
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called when the action is executing.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
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