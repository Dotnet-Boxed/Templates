namespace GraphQLTemplate.Schemas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQL.Builders;
    using GraphQL.Types;
    using GraphQL.Types.Relay.DataObjects;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>
    /// <example>
    /// The is an example query to get a human and the details of their friends.
    /// <c>
    /// query getHuman {
    ///   human(id: "94fbd693-2027-4804-bf40-ed427fe76fda")
    ///   {
    ///     id
    ///     name
    ///     dateOfBirth
    ///     homePlanet
    ///     appearsIn
    ///     friends {
    ///       name
    ///       ... on Droid {
    ///         chargePeriod
    ///         created
    ///         primaryFunction
    ///       }
    ///       ... on Human
    ///       {
    ///         dateOfBirth
    ///         homePlanet
    ///       }
    ///     }
    ///   }
    /// }
    /// </c>
    /// </example>
    public class QueryObject : ObjectGraphType<object>
    {
        private const int MaxPageSize = 10;

        public QueryObject(
            IDroidRepository droidRepository,
            IHumanRepository humanRepository)
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";

            this.FieldAsync<DroidObject, Droid>(
                "droid",
                "Get a droid by its unique identifier.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique identifier of the droid.",
                    }),
                resolve: context =>
                    droidRepository.GetDroid(
                        context.GetArgument("id", defaultValue: new Guid("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68")),
                        context.CancellationToken));
            this.FieldAsync<HumanObject, Human>(
                "human",
                "Get a human by its unique identifier.",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>()
                    {
                        Name = "id",
                        Description = "The unique identifier of the human.",
                    }),
                resolve: context => humanRepository.GetHuman(
                    context.GetArgument("id", defaultValue: new Guid("94fbd693-2027-4804-bf40-ed427fe76fda")),
                    context.CancellationToken));

            this.Connection<DroidObject>()
                .Name("droids")
                .Description("Gets pages of droids.")
                // Enable the last and before arguments to do paging in reverse.
                .Bidirectional()
                // Set the maximum size of a page, use .ReturnAll() to set no maximum size.
                .PageSize(MaxPageSize)
                .ResolveAsync(context => ResolveConnection(droidRepository, context));
        }

        private async static Task<object> ResolveConnection(
            IDroidRepository droidRepository,
            ResolveConnectionContext<object> context)
        {
            var first = context.First;
            var afterCursor = Cursor.FromCursor<DateTime?>(context.After);
            var last = context.Last;
            var beforeCursor = Cursor.FromCursor<DateTime?>(context.Before);
            var cancellationToken = context.CancellationToken;

            var getDroidsTask = GetDroids(droidRepository, first, afterCursor, last, beforeCursor, cancellationToken);
            var getHasNextPageTask = GetHasNextPage(droidRepository, first, afterCursor, cancellationToken);
            var getHasPreviousPageTask = GetHasPreviousPage(droidRepository, last, beforeCursor, cancellationToken);
            var totalCountTask = droidRepository.GetTotalCount(cancellationToken);

            await Task.WhenAll(getDroidsTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask);
            var droids = getDroidsTask.Result;
            var hasNextPage = getHasNextPageTask.Result;
            var hasPreviousPage = getHasPreviousPageTask.Result;
            var totalCount = totalCountTask.Result;
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(droids, x => x.Created);

            return new Connection<Droid>()
            {
                Edges = droids
                    .Select(x =>
                        new Edge<Droid>()
                        {
                            Cursor = Cursor.ToCursor(x.Created),
                            Node = x
                        })
                    .ToList(),
                PageInfo = new PageInfo()
                {
                    HasNextPage = hasNextPage,
                    HasPreviousPage = hasPreviousPage,
                    StartCursor = firstCursor,
                    EndCursor = lastCursor,
                },
                TotalCount = totalCount,
            };
        }

        private static Task<List<Droid>> GetDroids(
            IDroidRepository droidRepository,
            int? first,
            DateTime? afterCursor,
            int? last,
            DateTime? beforeCursor,
            CancellationToken cancellationToken)
        {
            Task<List<Droid>> getDroidsTask;
            if (first.HasValue)
            {
                getDroidsTask = droidRepository.GetDroids(first, afterCursor, cancellationToken);
            }
            else
            {
                getDroidsTask = droidRepository.GetDroidsReverse(last, beforeCursor, cancellationToken);
            }

            return getDroidsTask;
        }

        private static async Task<bool> GetHasNextPage(
            IDroidRepository droidRepository,
            int? first,
            DateTime? afterCursor,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return await droidRepository.GetHasNextPage(first, afterCursor, cancellationToken);
            }
            else
            {
                return false;
            }
        }

        private static async Task<bool> GetHasPreviousPage(
            IDroidRepository droidRepository,
            int? last,
            DateTime? beforeCursor,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return await droidRepository.GetHasPreviousPage(last, beforeCursor, cancellationToken);
            }
            else
            {
                return false;
            }
        }
    }
}
