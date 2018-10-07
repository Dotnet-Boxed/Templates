namespace ApiTemplate.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApiTemplate.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
#if (Swagger)
    using Swashbuckle.AspNetCore.Annotations;
#endif

    /// <summary>
    /// The status of this API.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
#if (Versioning)
    [ApiVersion("1.0")]
#endif
    public class StatusController : ControllerBase
    {
        private IEnumerable<IHealthChecker> healthCheckers;

        public StatusController(IEnumerable<IHealthChecker> healthCheckers) =>
            this.healthCheckers = healthCheckers;

        /// <summary>
        /// Gets the status of this API and its dependencies, giving an indication of its health.
        /// </summary>
        /// <returns>A 200 OK or error response containing details of what is wrong.</returns>
        [HttpGet(Name = StatusControllerRoute.GetStatus)]
        [AllowAnonymous]
#if (Swagger)
        [SwaggerResponse(StatusCodes.Status204NoContent, "The API is functioning normally.")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The API or one of its dependencies is not functioning, the service is unavailable.")]
#endif
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var tasks = new List<Task>();

                foreach (var healthChecker in this.healthCheckers)
                {
                    tasks.Add(healthChecker.CheckHealth());
                }

                await Task.WhenAll(tasks);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return new NoContentResult();
        }
    }
}