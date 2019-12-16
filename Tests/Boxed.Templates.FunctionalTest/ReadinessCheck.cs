namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;

    public static class ReadinessCheck
    {
        public static async Task<bool> StatusSelfAsync(HttpClient httpClient, HttpClient httpsClient)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpsClient is null)
            {
                throw new ArgumentNullException(nameof(httpsClient));
            }

            var response = await httpsClient
                .GetAsync(new Uri("/status/self", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> StatusSelfOverHttpAsync(HttpClient httpClient, HttpClient httpsClient)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpsClient is null)
            {
                throw new ArgumentNullException(nameof(httpsClient));
            }

            var response = await httpClient
                .GetAsync(new Uri("/status/self", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> FaviconAsync(HttpClient httpClient, HttpClient httpsClient)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpsClient is null)
            {
                throw new ArgumentNullException(nameof(httpsClient));
            }

            var response = await httpsClient
                .GetAsync(new Uri("/favicon.ico", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

        private static void LogStatusCode(HttpStatusCode statusCode) =>
            TestLogger.WriteMessage($"Waiting for app to start. Readiness check returned {(int)statusCode}.");
    }
}
