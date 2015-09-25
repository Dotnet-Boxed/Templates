namespace Boilerplate.Web.Mvc
{
    using System;
    using Microsoft.AspNet.Http;

    /// <summary>
    /// <see cref="HttpRequest"/> extension methods.
    /// </summary>
    public static class HttpRequestExtensions
    {
        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns><c>true</c> if the specified HTTP request is an AJAX request; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="request"/> parameter is <c>null</c>.</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }

            return false;
        }
    }
}
