namespace ApiTemplate.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApiTemplate.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The status of this API.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("[controller]")]
#if (Versioning)
    [ApiVersion("1.0")]
#endif
    public class StatusController : ControllerBase
    {
        private IEnumerable<IHealthChecker> healthCheckers;

        public StatusController(IEnumerable<IHealthChecker> healthCheckers) =>
            this.healthCheckers = healthCheckers;

        /// <summary>
        /// Gets the status of this API and it's dependencies, giving an indication of it's health.
        /// </summary>
        /// <returns>A 200 OK or error response containing details of what is wrong.</returns>
#if (Swagger)
        /// <response code="204">The API is functioning normally.</response>
        /// <response code="503">The API or one of it's dependencies is not functioning, the service is unavailable.</response>
#endif
        [HttpGet(Name = StatusControllerRoute.GetStatus)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status503ServiceUnavailable)]
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