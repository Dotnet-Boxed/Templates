namespace ApiTemplate.IntegrationTest.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.IntegrationTest.Fixtures;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Newtonsoft.Json;
    using Xunit;
    using Xunit.Abstractions;

    public class CarsControllerTest : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient client;
        private readonly MediaTypeFormatterCollection formatters;

        public CarsControllerTest(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            this.client = this.CreateClient();
            this.formatters = new MediaTypeFormatterCollection();
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentType.RestfulJson));
            this.formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(ContentType.ProblemJson));
        }

        [Fact]
        public async Task Options_CarsRoot_Returns200OkAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Options, "cars");

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(
                new string[] { HttpMethods.Get, HttpMethods.Head, HttpMethods.Options, HttpMethods.Post },
                response.Content.Headers.Allow);
        }

        [Fact]
        public async Task Options_CarsWithId_Returns200OkAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Options, "cars/1");

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

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
                    HttpMethods.Put,
                },
                response.Content.Headers.Allow);
        }

        [Fact]
        public async Task Delete_CarFound_Returns204NoContentAsync()
        {
            var car = new Models.Car();
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            this.CarRepositoryMock.Setup(x => x.DeleteAsync(car, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var response = await this.client.DeleteAsync(new Uri("/cars/1", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_CarNotFound_Returns404NotFoundAsync()
        {
            this.CarRepositoryMock.Setup(x => x.GetAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.DeleteAsync(new Uri("/cars/999", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }

        [Fact]
        public async Task Get_CarFound_Returns200OkAsync()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)) };
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.GetAsync(new Uri("/cars/1", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(new DateTimeOffset(2000, 1, 2, 3, 4, 5, TimeSpan.FromHours(6)), response.Content.Headers.LastModified);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task Get_CarNotFound_Returns404NotFoundAsync()
        {
            this.CarRepositoryMock.Setup(x => x.GetAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.GetAsync(new Uri("/cars/999", UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }

        [Fact]
        public async Task Get_InvalidAcceptHeader_Returns406NotAcceptableAsync()
        {
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);
            using var request = new HttpRequestMessage(HttpMethod.Get, "/cars/1");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.Text));

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotAcceptable, response.StatusCode);
            // Note: ASP.NET Core should be automatically returning a ProblemDetails response but is returning an empty
            // response body instead. See https://github.com/aspnet/AspNetCore/issues/16889
        }

        [Fact]
        public async Task Get_CarNotModifiedSince_Returns304NotModifiedAsync()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 1, 23, 59, 59, TimeSpan.Zero) };
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            using var request = new HttpRequestMessage(HttpMethod.Get, "/cars/1");
            request.Headers.IfModifiedSince = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero);

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotModified, response.StatusCode);
        }

        [Fact]
        public async Task Get_CarHasBeenModifiedSince_Returns200OKAsync()
        {
            var car = new Models.Car() { Modified = new DateTimeOffset(2000, 1, 1, 0, 0, 1, TimeSpan.Zero) };
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            using var request = new HttpRequestMessage(HttpMethod.Get, "/cars/1");
            request.Headers.IfModifiedSince = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/cars")]
        [InlineData("/cars?first=3")]
        [InlineData("/cars?first=3&after=THIS_IS_INVALID")]
        public async Task GetPage_FirstPage_Returns200OkAsync(string path)
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsAsync(3, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Take(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasNextPageAsync(3, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await this.client.GetAsync(new Uri(path, UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrlsAsync(
                    response,
#if HttpsEverywhere
                    nextPageUrl: "https://localhost/cars?First=3&After=MjAwMC0wMS0wM1QwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#else
                    nextPageUrl: "http://localhost/cars?First=3&After=MjAwMC0wMS0wM1QwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#endif
                    previousPageUrl: null)
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task GetPage_SecondPage_Returns200OkAsync()
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsAsync(3, new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero), null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Skip(3).Take(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasNextPageAsync(3, new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var response = await this.client
                .GetAsync(new Uri("/cars?first=3&after=MjAwMC0wMS0wM1QwMDowMDowMC4wMDAwMDAwKzAwOjAw", UriKind.Relative))
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrlsAsync(
                    response,
                    nextPageUrl: null,
#if HttpsEverywhere
                    previousPageUrl: "https://localhost/cars?First=3&Before=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#else
                    previousPageUrl: "http://localhost/cars?First=3&Before=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#endif
                    pageCount: 1)
                .ConfigureAwait(false);
        }

        [Theory]
        [InlineData("/cars?last=3")]
        [InlineData("/cars?last=3&before=THIS_IS_INVALID")]
        public async Task GetPage_LastPage_Returns200OkAsync(string path)
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsReverseAsync(3, null, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.TakeLast(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasPreviousPageAsync(3, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var response = await this.client.GetAsync(new Uri(path, UriKind.Relative)).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrlsAsync(
                    response,
                    nextPageUrl: null,
#if HttpsEverywhere
                    previousPageUrl: "https://localhost/cars?Last=3&Before=MjAwMC0wMS0wMlQwMDowMDowMC4wMDAwMDAwKzAwOjAw")
#else
                    previousPageUrl: "http://localhost/cars?Last=3&Before=MjAwMC0wMS0wMlQwMDowMDowMC4wMDAwMDAwKzAwOjAw")
#endif
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task GetPage_SecondLastPage_Returns200OkAsync()
        {
            var cars = GetCars();
            this.CarRepositoryMock
                .Setup(x => x.GetCarsReverseAsync(3, null, new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Skip(3).TakeLast(3).ToList());
            this.CarRepositoryMock
                .Setup(x => x.GetTotalCountAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(cars.Count);
            this.CarRepositoryMock
                .Setup(x => x.GetHasPreviousPageAsync(3, new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var response = await this.client
                .GetAsync(new Uri("/cars?last=3&before=MjAwMC0wMS0wMlQwMDowMDowMC4wMDAwMDAwKzAwOjAw", UriKind.Relative))
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            await this.AssertPageUrlsAsync(
                    response,
#if HttpsEverywhere
                    nextPageUrl: "https://localhost/cars?Last=3&After=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#else
                    nextPageUrl: "http://localhost/cars?Last=3&After=MjAwMC0wMS0wNFQwMDowMDowMC4wMDAwMDAwKzAwOjAw",
#endif
                    previousPageUrl: null,
                    pageCount: 1)
                .ConfigureAwait(false);
        }

        [Fact]
        public async Task PostCar_Valid_Returns201CreatedAsync()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic",
            };
            var car = new Models.Car() { CarId = 1 };
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock
                .Setup(x => x.AddAsync(It.IsAny<Models.Car>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(car);

            var response = await this.client.PostAsJsonAsync("cars", saveCar).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters).ConfigureAwait(false);
#if HttpsEverywhere
            Assert.Equal(new Uri("https://localhost/cars/1"), response.Headers.Location);
#else
            Assert.Equal(new Uri("http://localhost/cars/1"), response.Headers.Location);
#endif
        }

        [Fact]
        public async Task PostCar_Invalid_Returns400BadRequestAsync()
        {
            var response = await this.client.PostAsJsonAsync("cars", new SaveCar()).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status400BadRequest, problemDetails.Status);
        }

        [Fact]
        public async Task PostCar_EmptyRequestBody_Returns400BadRequestAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "cars")
            {
                Content = new ObjectContent<SaveCar>(null, new JsonMediaTypeFormatter(), ContentType.Json),
            };

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status400BadRequest, problemDetails.Status);
        }

        [Fact]
        public async Task PostCar_UnsupportedMediaType_Returns415UnsupportedMediaTypeAsync()
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "cars")
            {
                Content = new ObjectContent<SaveCar>(new SaveCar(), new JsonMediaTypeFormatter(), ContentType.Text),
            };

            var response = await this.client.SendAsync(request).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status415UnsupportedMediaType, problemDetails.Status);
        }

        [Fact]
        public async Task PutCar_Valid_Returns200OkAsync()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic",
            };
            var car = new Models.Car() { CarId = 1 };
            this.CarRepositoryMock
                .Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(car);
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock.Setup(x => x.UpdateAsync(car, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client.PutAsJsonAsync("cars/1", saveCar).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(ContentType.RestfulJson, response.Content.Headers.ContentType.MediaType);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters).ConfigureAwait(false);
        }

        [Fact]
        public async Task PutCar_CarNotFound_Returns404NotFoundAsync()
        {
            var saveCar = new SaveCar()
            {
                Cylinders = 2,
                Make = "Honda",
                Model = "Civic",
            };
            this.CarRepositoryMock.Setup(x => x.GetAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client.PutAsJsonAsync("cars/999", saveCar).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }

        [Fact]
        public async Task PutCar_Invalid_Returns400BadRequestAsync()
        {
            var response = await this.client.PutAsJsonAsync("cars/1", new SaveCar()).ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status400BadRequest, problemDetails.Status);
        }

        [Fact]
        public async Task PatchCar_CarNotFound_Returns404NotFoundAsync()
        {
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Remove(x => x.Make);
            var json = JsonConvert.SerializeObject(patch);
            using var content = new StringContent(json, Encoding.UTF8, ContentType.JsonPatch);
            this.CarRepositoryMock.Setup(x => x.GetAsync(999, It.IsAny<CancellationToken>())).ReturnsAsync((Models.Car)null);

            var response = await this.client
                .PatchAsync(new Uri("cars/999", UriKind.Relative), content)
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>(this.formatters).ConfigureAwait(false);
            Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        }

        [Fact]
        public async Task PatchCar_InvalidCar_Returns400BadRequestAsync()
        {
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Remove(x => x.Make);
            var json = JsonConvert.SerializeObject(patch);
            using var content = new StringContent(json, Encoding.UTF8, ContentType.JsonPatch);
            var car = new Models.Car();
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client
                .PatchAsync(new Uri("cars/1", UriKind.Relative), content)
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PatchCar_ValidCar_Returns200OkAsync()
        {
            var patch = new JsonPatchDocument<SaveCar>();
            patch.Add(x => x.Model, "Civic Type-R");
            var json = JsonConvert.SerializeObject(patch);
            using var content = new StringContent(json, Encoding.UTF8, ContentType.JsonPatch);
            var car = new Models.Car() { CarId = 1, Cylinders = 2, Make = "Honda", Model = "Civic" };
            this.CarRepositoryMock.Setup(x => x.GetAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(car);
            this.ClockServiceMock.SetupGet(x => x.UtcNow).Returns(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero));
            this.CarRepositoryMock.Setup(x => x.UpdateAsync(car, It.IsAny<CancellationToken>())).ReturnsAsync(car);

            var response = await this.client
                .PatchAsync(new Uri("cars/1", UriKind.Relative), content)
                .ConfigureAwait(false);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var carViewModel = await response.Content.ReadAsAsync<Car>(this.formatters).ConfigureAwait(false);
            Assert.Equal("Civic Type-R", carViewModel.Model);
        }

        private static List<Models.Car> GetCars() =>
            new List<Models.Car>()
            {
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 2, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 3, 0, 0, 0, TimeSpan.Zero) },
                new Models.Car() { Created = new DateTimeOffset(2000, 1, 4, 0, 0, 0, TimeSpan.Zero) },
            };

        private async Task AssertPageUrlsAsync(
            HttpResponseMessage response,
            string nextPageUrl,
            string previousPageUrl,
            int pageCount = 3,
            int requestedPageCount = 3,
            int totalCount = 4)
        {
            var connection = await response.Content.ReadAsAsync<Connection<Car>>(this.formatters).ConfigureAwait(false);

            Assert.Equal(pageCount, connection.Items.Count);
            Assert.Equal(pageCount, connection.PageInfo.Count);
            Assert.Equal(totalCount, connection.TotalCount);

            Assert.Equal(nextPageUrl is object, connection.PageInfo.HasNextPage);
            Assert.Equal(previousPageUrl is object, connection.PageInfo.HasPreviousPage);

            if (nextPageUrl is null)
            {
                Assert.Null(nextPageUrl);
            }
            else
            {
                Assert.Equal(new Uri(nextPageUrl), connection.PageInfo.NextPageUrl);
            }

            if (previousPageUrl is null)
            {
                Assert.Null(previousPageUrl);
            }
            else
            {
                Assert.Equal(new Uri(previousPageUrl), connection.PageInfo.PreviousPageUrl);
            }

#if HttpsEverywhere
            var firstPageUrl = $"https://localhost/cars?First={requestedPageCount}";
            var lastPageUrl = $"https://localhost/cars?Last={requestedPageCount}";
#else
            var firstPageUrl = $"http://localhost/cars?First={requestedPageCount}";
            var lastPageUrl = $"http://localhost/cars?Last={requestedPageCount}";
#endif

            Assert.Equal(new Uri(firstPageUrl), connection.PageInfo.FirstPageUrl);
            Assert.Equal(new Uri(lastPageUrl), connection.PageInfo.LastPageUrl);

            var linkValue = Assert.Single(response.Headers.GetValues("Link"));
            var expectedLink = $"<{firstPageUrl}>; rel=\"first\", <{lastPageUrl}>; rel=\"last\"";
            if (previousPageUrl is object)
            {
                expectedLink = $"<{previousPageUrl}>; rel=\"previous\", " + expectedLink;
            }

            if (nextPageUrl is object)
            {
                expectedLink = $"<{nextPageUrl}>; rel=\"next\", " + expectedLink;
            }

            Assert.Equal(expectedLink, linkValue);
        }
    }
}
