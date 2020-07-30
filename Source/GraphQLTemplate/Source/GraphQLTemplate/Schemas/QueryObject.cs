namespace GraphQLTemplate.Schemas
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphQLTemplate.Models;
    using GraphQLTemplate.Repositories;
    using GraphQLTemplate.Resolvers;
    using GraphQLTemplate.Types;
    using HotChocolate.Types;

    /// <summary>
    /// All queries defined in the schema used to retrieve data.
    /// </summary>
    /// <example>
    /// The is an example query to get a human and the details of their friends.
    /// <c>
    /// query getHuman {
    ///   human(id: "94fbd693-2027-4804-bf40-ed427fe76fda") {
    ///     id
    ///     name
    ///     dateOfBirth
    ///     homePlanet
    ///     appearsIn
    ///     friends {
    ///       id
    ///       name
    ///       ... on Droid {
    ///         primaryFunction
    ///         manufactured
    ///         chargePeriod
    ///       }
    ///       ... on Human {
    ///         dateOfBirth
    ///         homePlanet
    ///       }
    ///     }
    ///     created
    ///     modified
    ///   }
    /// }
    /// </c>
    /// It retrieves common properties, as well as properties specific to droids and humans.
    /// </example>
    public class QueryObject : ObjectType<QueryResolvers>
    {
        private const int MaxPageSize = 10;

        public QueryObject()
        {
            this.Name = "Query";
            this.Description = "The query type, represents all of the entry points into our object graph.";
        }

        protected override void Configure(IObjectTypeDescriptor<QueryResolvers> descriptor)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            descriptor
                .Field<QueryResolvers>(x => x.GetDroidAsync(default, default))
                .Type<DroidObject>()
                .Description("Get a droid by its unique identifier.")
                .Argument("id", x => x.Description("The unique identifier of the droid.").DefaultValue("1ae34c3b-c1a0-4b7b-9375-c5a221d49e68"));
            descriptor
                .Field<QueryResolvers>(x => x.GetHumanAsync(default, default))
                .Type<HumanObject>()
                .Description("Get a human by its unique identifier.")
                .Argument("id", x => x.Description("The unique identifier of the human.").DefaultValue("94fbd693-2027-4804-bf40-ed427fe76fda"));

            // descriptor.Connection<DroidObject>()
            //     .Name("droids")
            //     .Description("Gets pages of droids.")
            //     // Enable the last and before arguments to do paging in reverse.
            //     .Bidirectional()
            //     // Set the maximum size of a page, use .ReturnAll() to set no maximum size.
            //     .PageSize(MaxPageSize)
            //     .ResolveAsync(context => ResolveConnectionAsync(droidRepository, context));
        }

        // private static async Task<object> ResolveConnectionAsync(
        //     IDroidRepository droidRepository,
        //     ResolveConnectionContext<object> context)
        // {
        //     var first = context.First;
        //     var afterCursor = Cursor.FromCursor<DateTime?>(context.After);
        //     var last = context.Last;
        //     var beforeCursor = Cursor.FromCursor<DateTime?>(context.Before);
        //     var cancellationToken = context.CancellationToken;
        //
        //     var getDroidsTask = GetDroidsAsync(droidRepository, first, afterCursor, last, beforeCursor, cancellationToken);
        //     var getHasNextPageTask = GetHasNextPageAsync(droidRepository, first, afterCursor, cancellationToken);
        //     var getHasPreviousPageTask = GetHasPreviousPageAsync(droidRepository, last, beforeCursor, cancellationToken);
        //     var totalCountTask = droidRepository.GetTotalCountAsync(cancellationToken);
        //
        //     await Task.WhenAll(getDroidsTask, getHasNextPageTask, getHasPreviousPageTask, totalCountTask).ConfigureAwait(false);
        //     var droids = await getDroidsTask.ConfigureAwait(false);
        //     var hasNextPage = await getHasNextPageTask.ConfigureAwait(false);
        //     var hasPreviousPage = await getHasPreviousPageTask.ConfigureAwait(false);
        //     var totalCount = await totalCountTask.ConfigureAwait(false);
        //     var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(droids, x => x.Manufactured);
        //
        //     return new Connection<Droid>()
        //     {
        //         Edges = droids
        //             .Select(x =>
        //                 new Edge<Droid>()
        //                 {
        //                     Cursor = Cursor.ToCursor(x.Manufactured),
        //                     Node = x,
        //                 })
        //             .ToList(),
        //         PageInfo = new PageInfo()
        //         {
        //             HasNextPage = hasNextPage,
        //             HasPreviousPage = hasPreviousPage,
        //             StartCursor = firstCursor,
        //             EndCursor = lastCursor,
        //         },
        //         TotalCount = totalCount,
        //     };
        // }

        private static Task<List<Droid>> GetDroidsAsync(
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
                getDroidsTask = droidRepository.GetDroidsAsync(first, afterCursor, cancellationToken);
            }
            else
            {
                getDroidsTask = droidRepository.GetDroidsReverseAsync(last, beforeCursor, cancellationToken);
            }

            return getDroidsTask;
        }

        private static Task<bool> GetHasNextPageAsync(
            IDroidRepository droidRepository,
            int? first,
            DateTime? afterCursor,
            CancellationToken cancellationToken)
        {
            if (first.HasValue)
            {
                return droidRepository.GetHasNextPageAsync(first, afterCursor, cancellationToken);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        private static Task<bool> GetHasPreviousPageAsync(
            IDroidRepository droidRepository,
            int? last,
            DateTime? beforeCursor,
            CancellationToken cancellationToken)
        {
            if (last.HasValue)
            {
                return droidRepository.GetHasPreviousPageAsync(last, beforeCursor, cancellationToken);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
