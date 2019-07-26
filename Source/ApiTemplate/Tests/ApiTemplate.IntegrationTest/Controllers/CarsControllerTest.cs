namespace ApiTemplate.IntegrationTest.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ApiTemplate.IntegrationTest.Fixtures;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Xunit;

    public class CarsControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public CarsControllerTest(CustomWebApplicationFactory<Startup> factory) =>
            this.client = factory.CreateClient();

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
    }
}
