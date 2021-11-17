namespace Boxed.Templates.FunctionalTest;

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Boxed.DotnetNewTest;

public static class ReadinessCheck
{
    public static async Task<bool> StatusSelfAsync(HttpClient httpClient, HttpClient httpsClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(httpsClient);

        try
        {
            var response = await httpsClient
                .GetAsync(new Uri("/status/self", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }
        catch (Exception exception)
        {
            LogException(exception);
            return false;
        }
    }

    public static async Task<bool> StatusSelfOverHttpAsync(HttpClient httpClient, HttpClient httpsClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(httpsClient);

        try
        {
            var response = await httpClient
                .GetAsync(new Uri("/status/self", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }
        catch (Exception exception)
        {
            LogException(exception);
            return false;
        }
    }

    public static async Task<bool> FaviconAsync(HttpClient httpClient, HttpClient httpsClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(httpsClient);

        try
        {
            var response = await httpClient
                .GetAsync(new Uri("/favicon.ico", UriKind.Relative))
                .ConfigureAwait(false);
            LogStatusCode(response.StatusCode);
            return response.IsSuccessStatusCode;
        }
        catch (Exception exception)
        {
            LogException(exception);
            return false;
        }
    }

    private static void LogStatusCode(HttpStatusCode statusCode) =>
        TestLogger.WriteMessage?.Invoke(
            $"Waiting for app to start. Readiness check returned {(int)statusCode}.");

    private static void LogException(Exception exception) =>
        TestLogger.WriteMessage?.Invoke(
            $"Waiting for app to start. Readiness check threw {exception.GetType().Name}.{Environment.NewLine}{exception}");
}
