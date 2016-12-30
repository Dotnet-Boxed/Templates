#if (StatusController)
namespace MvcBoilerplate.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MvcBoilerplate.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The status of this API.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private IEnumerable<IConnectionTester> connectionTesters;

        public StatusController(IEnumerable<IConnectionTester> connectionTesters)
        {
            this.connectionTesters = connectionTesters;
        }

        /// <summary>
        /// Gets the status of this API and it's dependencies, giving an indication of it's health.
        /// </summary>
        /// <returns>A 200 OK or error response containing details of what is wrong.</returns>
        /// <response code="204">The API is functioning normally.</response>
        /// <response code="500">The API or one of it's dependencies is not functioning.</response>
        [HttpGet(Name = StatusControllerRoute.GetStatus)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStatus()
        {
            foreach (var connectionTester in this.connectionTesters)
            {
                await connectionTester.TestConnection();
            }

            return new NoContentResult();
        }
    }
}
#endif