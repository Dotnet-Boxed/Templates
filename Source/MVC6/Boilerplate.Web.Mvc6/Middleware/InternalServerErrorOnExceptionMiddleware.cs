namespace Boilerplate.Web.Mvc.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;

    internal class InternalServerErrorOnExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public InternalServerErrorOnExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
