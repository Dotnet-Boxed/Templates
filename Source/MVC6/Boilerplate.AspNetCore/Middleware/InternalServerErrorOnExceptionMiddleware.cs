namespace Boilerplate.AspNetCore.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

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
                await this.next.Invoke(context).ConfigureAwait(false);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
