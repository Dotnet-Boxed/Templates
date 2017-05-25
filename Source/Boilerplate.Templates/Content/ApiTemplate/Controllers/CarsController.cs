namespace ApiTemplate.Controllers
{
    using System;
    using System.Threading.Tasks;
    using ApiTemplate.Commands;
    using ApiTemplate.Constants;
    using ApiTemplate.ViewModels;
#if (RequestId || UserAgent)
    using Boilerplate.AspNetCore.Filters;
#endif
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;

    [Route("[controller]")]
#if (RequestId)
    // Require the X-Request-ID HTTP header to be set to a GUID and forward it in the response.
    [RequestIdHttpHeader]
#endif
#if (UserAgent)
    // Require the User-Agent HTTP header.
    [UserAgentHttpHeader]
#endif
#if (Versioning)
    [ApiVersion("1.0")]
#endif
    public class CarsController : ControllerBase
    {
        private readonly Lazy<IDeleteCarCommand> deleteCarCommand;
        private readonly Lazy<IGetCarCommand> getCarCommand;
        private readonly Lazy<IGetCarPageCommand> getCarPageCommand;
        private readonly Lazy<IPatchCarCommand> patchCarCommand;
        private readonly Lazy<IPostCarCommand> postCarCommand;
        private readonly Lazy<IPutCarCommand> putCarCommand;

        public CarsController(
            Lazy<IDeleteCarCommand> deleteCarCommand,
            Lazy<IGetCarCommand> getCarCommand,
            Lazy<IGetCarPageCommand> getCarPageCommand,
            Lazy<IPatchCarCommand> patchCarCommand,
            Lazy<IPostCarCommand> postCarCommand,
            Lazy<IPutCarCommand> putCarCommand)
        {
            this.deleteCarCommand = deleteCarCommand;
            this.getCarCommand = getCarCommand;
            this.getCarPageCommand = getCarPageCommand;
            this.patchCarCommand = patchCarCommand;
            this.postCarCommand = postCarCommand;
            this.putCarCommand = putCarCommand;
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        /// <response code="200">The allowed HTTP methods.</response>
        [HttpOptions]
        public IActionResult Options()
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return this.Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a car with the specified unique identifier.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        /// <response code="200">The allowed HTTP methods.</response>
        [HttpOptions("{carId}")]
        public IActionResult Options(int carId)
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);
            return this.Ok();
        }

        /// <summary>
        /// Deletes the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 204 No Content response if the car was deleted or a 404 Not Found if a car with the specified ID
        /// was not found.</returns>
#if (Swagger)
        /// <response code="204">The car with the specified ID was deleted.</response>
        /// <response code="404">A car with the specified ID was not found.</response>
#endif
        [HttpDelete("{carId}", Name = CarsControllerRoute.DeleteCar)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Delete(int carId) =>
            this.deleteCarCommand.Value.ExecuteAsync(carId);

        /// <summary>
        /// Gets the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified ID was not
        /// found.</returns>
#if (Swagger)
        /// <response code="200">The car with the specified ID.</response>
        /// <response code="304">The car has not changed since the date given in the If-Modified-Since HTTP header.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
#endif
        [HttpGet("{carId}", Name = CarsControllerRoute.GetCar)]
        [HttpHead("{carId}")]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status304NotModified)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(int carId) =>
            this.getCarCommand.Value.ExecuteAsync(carId);

        /// <summary>
        /// Gets a collection of cars using the specified page number and number of items per page.
        /// </summary>
        /// <param name="pageOptions">The page options.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
#if (Swagger)
        /// <response code="200">A collection of cars for the specified page.</response>
        /// <response code="400">The page request parameters are invalid.</response>
        /// <response code="404">A page with the specified page number was not found.</response>
#endif
        [HttpGet("", Name = CarsControllerRoute.GetCarPage)]
        [HttpHead("")]
        [ProducesResponseType(typeof(PageResult<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetPage([FromQuery] PageOptions pageOptions) =>
            this.getCarPageCommand.Value.ExecuteAsync(pageOptions);

        /// <summary>
        /// Patches the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com/.</param>
        /// <returns>A 200 OK if the car was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a car with the specified ID was not found.</returns>
#if (Swagger)
        /// <response code="200">The patched car with the specified ID.</response>
        /// <response code="400">The patch document is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
#endif
        [HttpPatch("{carId}", Name = CarsControllerRoute.PatchCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Patch(int carId, [FromBody] JsonPatchDocument<SaveCar> patch) =>
            this.patchCarCommand.Value.ExecuteAsync(carId, patch);

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="car">The car to create.</param>
        /// <returns>A 201 Created response containing the newly created car or a 400 Bad Request if the car is
        /// invalid.</returns>
#if (Swagger)
        /// <response code="201">The car was created.</response>
        /// <response code="400">The car is invalid.</response>
#endif
        [HttpPost("", Name = CarsControllerRoute.PostCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Post([FromBody] SaveCar car) =>
            this.postCarCommand.Value.ExecuteAsync(car);

        /// <summary>
        /// Updates an existing car with the specified ID.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <param name="car">The car to update.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified ID was not found.</returns>
#if (Swagger)
        /// <response code="200">The car was updated.</response>
        /// <response code="400">The car is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
#endif
        [HttpPut("{carId}", Name = CarsControllerRoute.PutCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Put(int carId, [FromBody] SaveCar car) =>
            this.putCarCommand.Value.ExecuteAsync(carId, car);
    }
}
