namespace ApiTemplate.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Commands;
    using ApiTemplate.Constants;
    using ApiTemplate.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
#if Swagger
    using Swashbuckle.AspNetCore.Annotations;
#endif

    [Route("[controller]")]
    [ApiController]
#if Versioning
    [ApiVersion(ApiVersionName.V1)]
#endif
#if Swagger
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
#endif
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CA1062 // Validate arguments of public methods
    public class CarsController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions(Name = CarsControllerRoute.OptionsCars)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
#endif
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
        /// <param name="carId">The cars unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{carId}", Name = CarsControllerRoute.OptionsCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
#endif
#pragma warning disable IDE0060, CA1801 // Remove unused parameter
        public IActionResult Options(int carId)
#pragma warning restore IDE0060, CA1801 // Remove unused parameter
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
        /// Deletes the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="carId">The cars unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the car was deleted or a 404 Not Found if a car with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{carId}", Name = CarsControllerRoute.DeleteCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status204NoContent, "The car with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier was not found.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> DeleteAsync(
            [FromServices] DeleteCarCommand command,
            int carId,
            CancellationToken cancellationToken) => command.ExecuteAsync(carId, cancellationToken);

        /// <summary>
        /// Gets the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="carId">The cars unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{carId}", Name = CarsControllerRoute.GetCar)]
        [HttpHead("{carId}", Name = CarsControllerRoute.HeadCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "The car with the specified unique identifier.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The car has not changed since the date given in the If-Modified-Since HTTP header.", typeof(void))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> GetAsync(
            [FromServices] GetCarCommand command,
            int carId,
            CancellationToken cancellationToken) => command.ExecuteAsync(carId, cancellationToken);

        /// <summary>
        /// Gets a collection of cars.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = CarsControllerRoute.GetCarPage)]
        [HttpHead("", Name = CarsControllerRoute.HeadCarPage)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of cars for the specified page.", typeof(Connection<Car>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> GetPageAsync(
            [FromServices] GetCarPageCommand command,
            [FromQuery] PageOptions pageOptions,
            CancellationToken cancellationToken) => command.ExecuteAsync(pageOptions, cancellationToken);

        /// <summary>
        /// Patches the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="carId">The cars unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK if the car was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a car with the specified unique identifier was not found.</returns>
        [HttpPatch("{carId}", Name = CarsControllerRoute.PatchCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "The patched car with the specified unique identifier.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> PatchAsync(
            [FromServices] PatchCarCommand command,
            int carId,
            [FromBody] JsonPatchDocument<SaveCar> patch,
            CancellationToken cancellationToken) => command.ExecuteAsync(carId, patch, cancellationToken);

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="car">The car to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created car or a 400 Bad Request if the car is
        /// invalid.</returns>
        [HttpPost("", Name = CarsControllerRoute.PostCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status201Created, "The car was created.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The car is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> PostAsync(
            [FromServices] PostCarCommand command,
            [FromBody] SaveCar car,
            CancellationToken cancellationToken) => command.ExecuteAsync(car, cancellationToken);

        /// <summary>
        /// Updates an existing car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="carId">The car identifier.</param>
        /// <param name="car">The car to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified unique identifier was not found.</returns>
        [HttpPut("{carId}", Name = CarsControllerRoute.PutCar)]
#if Swagger
        [SwaggerResponse(StatusCodes.Status200OK, "The car was updated.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The car is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status406NotAcceptable, "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType, "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
#endif
        public Task<IActionResult> PutAsync(
            [FromServices] PutCarCommand command,
            int carId,
            [FromBody] SaveCar car,
            CancellationToken cancellationToken) => command.ExecuteAsync(carId, car, cancellationToken);
    }
}
#pragma warning restore CA1062 // Validate arguments of public methods
#pragma warning restore CA1822 // Mark members as static
