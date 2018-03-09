namespace ApiTemplate
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class LastModifiedFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var resource = (context.Result as ObjectResult)?.Value as IModifiableResource;
            if (resource.HasBeenModified(context.HttpContext.Request))
            {
                resource.SetModifiedHttpHeaders(context.HttpContext.Response);
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status304NotModified);
            }
        }
    }
}
