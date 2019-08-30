namespace ApiTemplate.IntegrationTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.IntegrationTest.Fixtures;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Moq;
    using Xunit;

    public class CarsControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly HttpClient client;
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly Mock<ICarRepository> carRepositoryMock;
        private readonly MediaTypeFormatterCollection formatters;

        public CarsControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();
            this.carRepositoryMock = this.factory.CarRepositoryMock;

            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentType.RestfulJson));
        }

        [Fact]
        public async Task Delete_CarFound_Returns204NoContent()
        {
            var car = new Models.Car();
            this.carRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            this.carRepositoryMock.Setup(x => x.Delete(car, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var response = await this.client.DeleteAsync("/cars/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_CarNotFound_Returns404NotFound()
        {
            this.carRepositoryMock.Setup(x => x.Get(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.DeleteAsync("/cars/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetPage_Default_Returns200Ok()
        {
            var cars = new List<Models.Car>() { new Models.Car(), new Models.Car(), new Models.Car(), new Models.Car() };
            this.carRepositoryMock
                .Setup(x => x.GetCars(3, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Take(3).ToList());
            this.carRepositoryMock
                .Setup(x => x.GetTotalCount(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.carRepositoryMock
                .Setup(x => x.GetHasNextPage(3, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await this.client.GetAsync("/cars");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var connection = await response.Content.ReadAsAsync<Connection<Car>>(this.formatters);
            Assert.Equal(3, connection.Items.Count);
            Assert.Equal(3, connection.PageInfo.Count);
            Assert.Equal(new Uri("https://localhost/cars?First=3"), connection.PageInfo.FirstPageUrl);
            Assert.True(connection.PageInfo.HasNextPage);
            Assert.False(connection.PageInfo.HasPreviousPage);
            Assert.Equal(new Uri("https://localhost/cars?Last=3"), connection.PageInfo.LastPageUrl);
            Assert.Equal(new Uri("https://localhost/cars?First=3&After=MDEvMDEvMDAwMSAwMDowMDowMCArMDA6MDA%3D"), connection.PageInfo.NextPageUrl);
            Assert.Null(connection.PageInfo.PreviousPageUrl);
            Assert.Equal(4, connection.TotalCount);
        }

        [Fact]
        public async Task PostCar_Valid_Returns201Created()
        {
            var car = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };

            var response = await this.client.PostAsJsonAsync("cars", car);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
        }

        public void Dispose() => this.factory.VerifyAllMocks();
    }
}
