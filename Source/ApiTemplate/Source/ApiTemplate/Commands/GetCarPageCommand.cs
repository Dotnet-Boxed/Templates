namespace ApiTemplate.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using ApiTemplate.Constants;
    using ApiTemplate.Repositories;
    using ApiTemplate.ViewModels;
    using Boxed.AspNetCore;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class GetCarPageCommand : IGetCarPageCommand
    {
        private const string LinkHttpHeaderName = "Link";
        private const int DefaultPageSize = 3;
        private readonly ICarRepository carRepository;
        private readonly IMapper<Models.Car, Car> carMapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;

        public GetCarPageCommand(
            ICarRepository carRepository,
            IMapper<Models.Car, Car> carMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            this.carRepository = carRepository;
            this.carMapper = carMapper;
            this.httpContextAccessor = httpContextAccessor;
            this.urlHelper = urlHelper;
        }

        public async Task<IActionResult> ExecuteAsync(PageOptions pageOptions, CancellationToken cancellationToken)
        {
            pageOptions.First = !pageOptions.First.HasValue && !pageOptions.Last.HasValue ? DefaultPageSize : pageOptions.First;
            var createdAfter = Cursor.FromCursor<DateTimeOffset?>(pageOptions.After);
            var createdBefore = Cursor.FromCursor<DateTimeOffset?>(pageOptions.Before);

            var getCarsTask = this.GetCars(pageOptions.First, pageOptions.Last, createdAfter, createdBefore, cancellationToken);
            var getHasNextPageTask = this.GetHasNextPage(pageOptions.First, createdAfter, createdBefore, cancellationToken);
            var getHasPreviousPageTask = this.GetHasPreviousPage(pageOptions.Last, createdAfter, createdBefore, cancellationToken);
            var totalCountTask = this.carRepository.GetTotalCount(cancellationToken);

            await Task.WhenAll(getCarsTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask);
            var cars = getCarsTask.Result;
            var hasNextPage = getHasNextPageTask.Result;
            var hasPreviousPage = getHasPreviousPageTask.Result;
            var totalCount = totalCountTask.Result;

            if (cars == null)
            {
                return new NotFoundResult();
            }

            var (startCursor, endCursor) = Cursor.GetFirstAndLastCursor(cars, x => x.Created);
            var carViewModels = this.carMapper.MapList(cars);

            var connection = new Connection<Car>()
            {
                Items = carViewModels,
                PageInfo = new PageInfo()
                {
                    Count = carViewModels.Count,
                    HasNextPage = hasNextPage,
                    HasPreviousPage = hasPreviousPage,
                    NextPageUrl = hasNextPage ? new Uri(this.urlHelper.AbsoluteRouteUrl(
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            First = pageOptions.First,
                            Last = pageOptions.Last,
                            After = endCursor,
                        })) : null,
                    PreviousPageUrl = hasPreviousPage ? new Uri(this.urlHelper.AbsoluteRouteUrl(
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            First = pageOptions.First,
                            Last = pageOptions.Last,
                            Before = startCursor
                        })) : null,
                    FirstPageUrl = new Uri(this.urlHelper.AbsoluteRouteUrl(
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            First = pageOptions.First ?? pageOptions.Last,
                        })),
                    LastPageUrl = new Uri(this.urlHelper.AbsoluteRouteUrl(
                        CarsControllerRoute.GetCarPage,
                        new PageOptions()
                        {
                            Last = pageOptions.First ?? pageOptions.Last,
                        })),
                },
                TotalCount = totalCount,
            };

            this.httpContextAccessor.HttpContext.Response.Headers.Add(
                LinkHttpHeaderName,
                connection.PageInfo.ToLinkHttpHeaderValue());

            return new OkObjectResult(connection);
        }

        private Task<List<Models.Car>> GetCars(
            int? first,
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken)
        {
            Task<List<Models.Car>> getCarsTask;
            if (first.HasValue)
            {
                getCarsTask = this.carRepository.GetCars(first, createdAfter, createdBefore, cancellationToken);
            }
            else
            {
                getCarsTask = this.carRepository.GetCarsReverse(last, createdAfter, createdBefore, cancellationToken);
            }

            return getCarsTask;
        }

        private async Task<bool> GetHasNextPage(
            int? first,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await this.carRepository.GetHasNextPage(first, createdAfter, cancellationToken);
            }
            else if (createdBefore.HasValue)
            {
                return true;
            }

            return false;
        }

        private async Task<bool> GetHasPreviousPage(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await this.carRepository.GetHasPreviousPage(last, createdBefore, cancellationToken);
            }
            else if (createdAfter.HasValue)
            {
                return true;
            }

            return false;
        }
    }
}
