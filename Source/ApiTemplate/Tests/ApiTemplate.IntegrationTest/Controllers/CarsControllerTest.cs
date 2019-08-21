namespace ApiTemplate.IntegrationTest.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
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

        public CarsControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();
            this.carRepositoryMock = this.factory.CarRepositoryMock;
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
            var response = await this.client.GetAsync("/cars");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
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
