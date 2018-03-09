namespace ApiTemplate
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Net.Http.Headers;

    public static class ModifiableResourceExtensions
    {
        public static string GetWeakETag(this IModifiableResource resource, string id) =>
            $"\"{id}-{resource.LastModified.Value.Ticks.ToString(CultureInfo.InvariantCulture)}\"";

        public static bool HasBeenModified(
            this IModifiableResource resource,
            HttpRequest request)
        {
            var modified = true;

            var requestHeaders = request.GetTypedHeaders();
            if (HttpMethods.IsGet(request.Method) || HttpMethods.IsHead(request.Method))
            {
                var ifNoneMatch = requestHeaders.IfNoneMatch?.Select(v => v.Tag.ToString());
                var ifModifiedSince = requestHeaders.IfModifiedSince;

                if ((ifNoneMatch != null) && ifNoneMatch.Any())
                {
                    if (!string.IsNullOrWhiteSpace(resource.ETag)
                        && ifNoneMatch.Contains(resource.ETag))
                    {
                        modified = false;
                    }
                }
                else if (ifModifiedSince.HasValue && resource.LastModified.HasValue)
                {
                    var lastModified = resource.LastModified.Value.AddTicks(
                        -(resource.LastModified.Value.Ticks % TimeSpan.TicksPerSecond));
                    if (lastModified <= ifModifiedSince.Value)
                    {
                        modified = false;
                    }
                }
            }

            return modified;
        }

        public static bool HasPreconditionFailed(
            this IModifiableResource resource,
            HttpRequest request)
        {
            bool preconditionFailed = false;

            var requestHeaders = request.GetTypedHeaders();
            if (HttpMethods.IsPut(request.Method) || HttpMethods.IsPatch(request.Method))
            {
                var ifMatch = requestHeaders.IfMatch?.Select(v => v.Tag.ToString());
                var ifUnmodifiedSince = requestHeaders.IfUnmodifiedSince;

                if ((ifMatch) != null && ifMatch.Any())
                {
                    if ((ifMatch.Count() > 2) || (ifMatch.First() != "*"))
                    {
                        if (!ifMatch.Contains(resource.ETag))
                        {
                            preconditionFailed = true;
                        }
                    }
                }
                else if (ifUnmodifiedSince.HasValue)
                {
                    DateTimeOffset lastModified = resource.LastModified.Value.AddTicks(
                        -(resource.LastModified.Value.Ticks % TimeSpan.TicksPerSecond));

                    if (lastModified > ifUnmodifiedSince.Value)
                    {
                        preconditionFailed = true;
                    }
                }
            }

            return preconditionFailed;
        }

        public static void SetModifiedHttpHeaders(this IModifiableResource resource, HttpResponse response)
        {
            var etag = resource.ETag;
            if (etag != null && resource.LastModified.HasValue)
            {
                var responseHeaders = response.GetTypedHeaders();
                if (etag != null)
                {
                    responseHeaders.ETag = new EntityTagHeaderValue(etag, true);
                }

                responseHeaders.LastModified = resource.LastModified.Value;
            }
        }
    }
}
