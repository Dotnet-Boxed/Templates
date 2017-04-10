namespace ApiTemplate.Commands
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApiTemplate.Constants;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Boilerplate;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GetCarPageCommand : IGetCarPageCommand
    {
        private readonly ICarRepository carRepository;
        private readonly ITranslator<Models.Car, Car> carTranslator;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;

        public GetCarPageCommand(
            ICarRepository carRepository,
            ITranslator<Models.Car, Car> carTranslator,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            this.carRepository = carRepository;
            this.carTranslator = carTranslator;
            this.httpContextAccessor = httpContextAccessor;
            this.urlHelper = urlHelper;
        }

        public async Task<IActionResult> ExecuteAsync(PageOptions pageOptions)
        {
            var cars = await this.carRepository.GetPage(pageOptions.Page, pageOptions.Count);
            if (cars == null)
            {
                return new NotFoundResult();
            }

            var totalPages = await this.carRepository.GetTotalPages(pageOptions.Count);
            var carViewModels = this.carTranslator.TranslateList(cars);
            var page = new PageResult<Car>()
            {
                Count = pageOptions.Count,
                Items = carViewModels,
                Page = pageOptions.Page,
                Total = totalPages
            };

            // Add the Link HTTP Header to add URL's to next, previous, first and last pages.
            // See https://tools.ietf.org/html/rfc5988#page-6
            // There is a standard list of link relation types e.g. next, previous, first and last.
            // See https://www.iana.org/assignments/link-relations/link-relations.xhtml
            this.httpContextAccessor.HttpContext.Response.Headers.Add(
                "Link",
                this.GetLinkValue(page));

            return new OkObjectResult(page);
        }

        private string GetLinkValue(PageResult<Car> page)
        {
            var values = new List<string>(4);

            if (page.HasNextPage)
            {
                values.Add(this.GetLinkValueItem("next", page.Page + 1, page.Count));
            }

            if (page.HasPreviousPage)
            {
                values.Add(this.GetLinkValueItem("previous", page.Page - 1, page.Count));
            }

            values.Add(this.GetLinkValueItem("first", 1, page.Count));
            values.Add(this.GetLinkValueItem("last", page.Total, page.Count));

            return string.Join(", ", values);
        }

        private string GetLinkValueItem(string rel, int page, int count)
        {
            var url = this.urlHelper.RouteUrl(
                CarsControllerRoute.GetCarPage,
                new PageOptions { Page = page, Count = count });
            return $"<{url}>; rel=\"{rel}\"";
        }
    }
}
