namespace Boilerplate.AspNetCore.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    internal class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public HttpExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context).ConfigureAwait(false);
            }
            catch (HttpException httpException)
            {
                context.Response.StatusCode = httpException.StatusCode;
                if (httpException != null)
                {
                    var responseFeature = context.Features.Get<IHttpResponseFeature>();
                    responseFeature.ReasonPhrase = httpException.Message;
                }
            }
        }
    }
}