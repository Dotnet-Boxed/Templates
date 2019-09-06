namespace ApiTemplate.IntegrationTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.IntegrationTest.Fixtures;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;

    public class CarsControllerTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;
        private readonly MediaTypeFormatterCollection formatters;

        public CarsControllerTest()
        {
            this.client = this.CreateClient();
            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(
                new System.Net.Http.Headers.MediaTypeHeaderValue(ContentType.RestfulJson));
        }

        [Fact]
        public async Task Options_CarsRoot_Returns200Ok()
        {
            var response = await this.client.SendAsync(new HttpRequestMessage(HttpMethod.Options, "cars"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(
                new string[] { HttpMethods.Get, HttpMethods.Head, HttpMethods.Options, HttpMethods.Post },
                response.Content.Headers.Allow);
        }

        [Fact]
        public async Task Options_CarsWithId_Returns200Ok()
        {
            var response = await this.client.SendAsync(new HttpRequestMessage(HttpMethod.Options, "cars/1"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(
                new string[]
                {
                    HttpMethods.Delete,
                    HttpMethods.Get,
                    HttpMethods.Head,
                    HttpMethods.Options,
                    HttpMethods.Patch,
                    HttpMethods.Post,
                    HttpMethods.Put
                },
                response.Content.Headers.Allow);
        }

        [Fact]
        public async Task Delete_CarFound_Returns204NoContent()
        {
            var car = new Models.Car();
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            this.CarRepositoryMock.Setup(x => x.Delete(car, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var response = await this.client.DeleteAsync("/cars/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_CarNotFound_Returns404NotFound()
        {
            this.CarRepositoryMock.Setup(x => x.Get(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.DeleteAsync("/cars/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_CarFound_Returns200Ok()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)) };
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.GetAsync("/cars/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)), response.Content.Headers.LastModified);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters);
        }

        [Fact]
        public async Task Get_CarNotFound_Returns404NotFound()
        {
            this.CarRepositoryMock.Setup(x => x.Get(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.GetAsync("/cars/999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_CarNotModifiedSince_Returns304NotModified()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.Zero) };
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            var request = new HttpRequestMessage(HttpMethod.Get, "/cars/1");
            request.Headers.IfModifiedSince = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero);

            var response = await this.client.SendAsync(request);

            Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
        }

        [Fact]
        public async Task Get_CarHasBeenModifiedSince_Returns200OK()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 1, 0, 0, 1, TimeSpan.Zero) };
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            var request = new HttpRequestMessage(HttpMethod.Get, "/cars/1");
            request.Headers.IfModifiedSince = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var response = await this.client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/cars")]
        [InlineData("/cars?first=3")]
        [InlineData("/cars?first=3&after=THIS_IS_INVALID")]
        public async Task GetPage_FirstPage_Returns200Ok(string uri)
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCars(3, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Take(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCount(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasNextPage(3, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await this.client.GetAsync(uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrls(
                response,
                nextPageUrl: "https://localhost/cars?First=3&After=MjAwMC0wMS0wM1QwMDowMDowMC4wMDAwMDAwKzAwOjAw",
                previousPageUrl: null);
        }

        [Fact]
        public async Task GetPage_SecondPage_Returns200Ok()
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCars(3, new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero), null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Skip(3).Take(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCount(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasNextPage(3, new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var response = await this.client.GetAsync("/cars?first=3&after=MjAwMC0wMS0wM1QwMDowMDowMC4wMDAwMDAwKzAwOjAw");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrls(
                response,
                nextPageUrl: null,
                previousPageUrl: "https://localhost/cars?First=3&Before=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
                pageCount: 1);
        }

        [Theory]
        [InlineData("/cars?last=3")]
        [InlineData("/cars?last=3&before=THIS_IS_INVALID")]
        public async Task GetPage_LastPage_Returns200Ok(string uri)
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsReverse(3, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.TakeLast(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCount(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasPreviousPage(3, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await this.client.GetAsync(uri);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrls(
                response,
                nextPageUrl: null,
                previousPageUrl: "https://localhost/cars?Last=3&Before=MjAwMC0wMS0wMlQwMDowMDowMC4wMDAwMDAwKzAwOjAw");
        }

        [Fact]
        public async Task GetPage_SecondLastPage_Returns200Ok()
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsReverse(3, null, new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Skip(3).TakeLast(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCount(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasPreviousPage(3, new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var response = await this.client.GetAsync("/cars?last=3&before=MjAwMC0wMS0wMlQwMDowMDowMC4wMDAwMDAwKzAwOjAw");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrls(
                response,
                nextPageUrl: "https://localhost/cars?Last=3&After=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
                previousPageUrl: null,
                pageCount: 1);
        }

        [Fact]
        public async Task PostCar_Valid_Returns201Created()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            var car = new Models.Car() { CarId = 1 };
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock
                .Setup(x => x.Add(It.IsAny<Models.Car>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(car);

            var response = await this.client.PostAsJsonAsync("cars", saveCar);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters);
            Assert.Equal(new Uri("https://localhost/cars/1"), response.Headers.Location);
        }

        [Fact]
        public async Task PostCar_Invalid_Returns400BadRequest()
        {
            var response = await this.client.PostAsJsonAsync("cars", new SaveCar());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutCar_Valid_Returns200Ok()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            var car = new Models.Car() { CarId = 1 };
            this.CarRepositoryMock
                .Setup(x => x.Get(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(car);
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock.Setup(x => x.Update(car, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.PutAsJsonAsync("cars/1", saveCar);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters);
        }

        [Fact]
        public async Task PutCar_CarNotFound_Returns404NotFound()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            this.CarRepositoryMock.Setup(x => x.Get(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.PutAsJsonAsync("cars/999", saveCar);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutCar_Invalid_Returns400BadRequest()
        {
            var response = await this.client.PutAsJsonAsync("cars/1", new SaveCar());

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PatchCar_CarNotFound_Returns404NotFound()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Remove(x => x.Make);
            var json = JsonConvert.SerializeObject(patch);
            this.CarRepositoryMock.Setup(x => x.Get(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.PatchAsync("cars/999", new StringContent(json, Encoding.UTF8, ContentType.Json));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PatchCar_InvalidCar_Returns400BadRequest()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Remove(x => x.Make);
            var json = JsonConvert.SerializeObject(patch);
            var car = new Models.Car();
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.PatchAsync("cars/1", new StringContent(json, Encoding.UTF8, ContentType.Json));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PatchCar_ValidCar_Returns200Ok()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic"
            };
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Add(x => x.Model, "Civic Type-R");
            var json = JsonConvert.SerializeObject(patch);
            var car = new Models.Car() { CarId = 1, Cylinders = 2, Make = "Honda", Model = "Civic" };
            this.CarRepositoryMock.Setup(x => x.Get(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock.Setup(x => x.Update(car, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.PatchAsync("cars/1", new StringContent(json, Encoding.UTF8, ContentType.Json));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters);
            Assert.Equal("Civic Type-R", carViewModel.Model);
        }

        private async Task AssertPageUrls(
            HttpResponseMessage response,
            string nextPageUrl,
            string previousPageUrl,
            int pageCount = 3,
            int requestedPageCount = 3,
            int totalCount = 4)
        {
            var connection = await response.Content.ReadAsAsync<Connection<Car>>(this.formatters);

            Assert.Equal(pageCount, connection.Items.Count);
            Assert.Equal(pageCount, connection.PageInfo.Count);
            Assert.Equal(totalCount, connection.TotalCount);

            Assert.Equal(nextPageUrl != null, connection.PageInfo.HasNextPage);
            Assert.Equal(previousPageUrl != null, connection.PageInfo.HasPreviousPage);

            if (nextPageUrl == null)
            {
                Assert.Null(nextPageUrl);
            }
            else
            {
                Assert.Equal(new Uri(nextPageUrl), connection.PageInfo.NextPageUrl);
            }

            if (previousPageUrl == null)
            {
                Assert.Null(previousPageUrl);
            }
            else
            {
                Assert.Equal(new Uri(previousPageUrl), connection.PageInfo.PreviousPageUrl);
            }

            var firstPageUrl = $"https://localhost/cars?First={requestedPageCount}";
            var lastPageUrl = $"https://localhost/cars?Last={requestedPageCount}";

            Assert.Equal(new Uri(firstPageUrl), connection.PageInfo.FirstPageUrl);
            Assert.Equal(new Uri(lastPageUrl), connection.PageInfo.LastPageUrl);

            var linkValue = Assert.Single(response.Headers.GetValues("Link"));
            var expectedLink = $"<{firstPageUrl}>; rel=\"first\", <{lastPageUrl}>; rel=\"last\"";
            if (previousPageUrl != null)
            {
                expectedLink = $"<{previousPageUrl}>; rel=\"previous\", " + expectedLink;
            }

            if (nextPageUrl != null)
            {
                expectedLink = $"<{nextPageUrl}>; rel=\"next\", " + expectedLink;
            }

            Assert.Equal(expectedLink, linkValue);
        }

        private static List<Models.Car> GetCars() =>
            new List<Models.Car>()
            {
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 4, 0, 0, 0, TimeSpan.Zero) },
            };
    }
}
