namespace Boilerplate.AspNetCore.Formatters
{
    using Microsoft.Net.Http.Headers;

    internal static class MediaTypeHeaderValues
    {
        public static readonly MediaTypeHeaderValue ApplicationBson
            = MediaTypeHeaderValue.Parse("application/bson").CopyAsReadOnly();
    }
}
