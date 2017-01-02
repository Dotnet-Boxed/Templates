namespace ApiTemplate.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using ApiTemplate.Commands;
    using ApiTemplate.Constants;
    using ApiTemplate.ViewModels;

    [Route("[controller]")]
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
        /// Deletes the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 204 No Content response if the car was deleted or a 404 Not Found if a car with the specified ID
        /// was not found.</returns>
        /// <response code="204">The car with the specified ID was deleted.</response>
        /// <response code="404">A car with the specified ID was not found.</response>
        [HttpDelete("{carId}", Name = CarsControllerRoute.DeleteCar)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Delete(int carId)
        {
            return this.deleteCarCommand.Value.ExecuteAsync(carId);
        }

        /// <summary>
        /// Gets the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified ID was not
        /// found.</returns>
        /// <response code="200">The car with the specified ID.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpGet("{carId}", Name = CarsControllerRoute.GetCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get(int carId)
        {
            return this.getCarCommand.Value.ExecuteAsync(carId);
        }

        /// <summary>
        /// Gets a collection of cars using the specified page number and number of items per page.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        /// <response code="200">A collection of cars for the specified page.</response>
        /// <response code="400">The page request parameters are invalid.</response>
        /// <response code="404">A page with the specified page number was not found.</response>
        [HttpGet("", Name = CarsControllerRoute.GetCarPage)]
        [ProducesResponseType(typeof(Page<Car>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> GetPage(PageRequest pageRequest)
        {
            return this.getCarPageCommand.Value.ExecuteAsync(pageRequest);
        }

        /// <summary>
        /// Patches the car with the specified ID.
        /// </summary>
        /// <param name="carId">The car ID.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com/.</param>
        /// <returns>A 200 OK if the car was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a car with the specified ID was not found.</returns>
        /// <response code="200">The patched car with the specified ID.</response>
        /// <response code="400">The patch document is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpPatch("{carId}", Name = CarsControllerRoute.PatchCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Patch(int carId, [FromBody] JsonPatchDocument<SaveCar> patch)
        {
            return this.patchCarCommand.Value.ExecuteAsync(carId, patch);
        }

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="car">The car to create.</param>
        /// <returns>A 201 Created response containing the newly created car or a 400 Bad Request if the car is
        /// invalid.</returns>
        /// <response code="201">The car was created.</response>
        /// <response code="400">The car is invalid.</response>
        [HttpPost("", Name = CarsControllerRoute.PostCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Post([FromBody] SaveCar car)
        {
            return this.postCarCommand.Value.ExecuteAsync(car);
        }

        /// <summary>
        /// Updates an existing car with the specified ID.
        /// </summary>
        /// <param name="car">The car to update.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified ID was not found.</returns>
        /// <response code="200">The car was updated.</response>
        /// <response code="400">The car is invalid.</response>
        /// <response code="404">A car with the specified ID could not be found.</response>
        [HttpPost("{carId}", Name = CarsControllerRoute.PutCar)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Car), StatusCodes.Status404NotFound)]
        public Task<IActionResult> Put(int carId, [FromBody] SaveCar car)
        {
            return this.putCarCommand.Value.ExecuteAsync(carId, car);
        }
    }
}
