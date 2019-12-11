namespace Boxed.Templates.FunctionalTest
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Boxed.DotnetNewTest;

    public static class ReadinessCheck
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static async Task<bool> StatusSelf(HttpClient httpClient, HttpClient httpsClient)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var response = await httpsClient.GetAsync("/status/self");
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public static async Task<bool> StatusSelfOverHttp(HttpClient httpClient, HttpClient httpsClient)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var response = await httpClient.GetAsync("/status/self");
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public static async Task<bool> Favicon(HttpClient httpClient, HttpClient httpsClient)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var response = await httpsClient.GetAsync("/favicon.ico");
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }

        private static void LogStatusCode(HttpStatusCode statusCode) =>
            TestLogger.WriteMessage($"Waiting for app to start. Readiness check returned {(int)statusCode}.");
    }
}
