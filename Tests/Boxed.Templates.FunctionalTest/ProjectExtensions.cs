namespace Boxed.Templates.FunctionalTest;

using System;
using System.Threading.Tasks;
using Boxed.DotnetNewTest;

public static class ProjectExtensions
{
    private const int DefaultRetries = 2;

    public static async Task DotnetRestoreWithRetryAsync(this Project project, TimeSpan? timeout = null, bool showShellWindow = false)
    {
        for (var i = 0; i < DefaultRetries; ++i)
        {
            try
            {
                await project.DotnetRestoreAsync(timeout, showShellWindow).ConfigureAwait(false);
                return;
            }
            catch (Exception exception)
            {
                TestLogger.WriteMessage?.Invoke($"Retry {i} failed with exception:{Environment.NewLine}{exception}");

                if (i == DefaultRetries - 1)
                {
                    throw;
                }
            }
        }
    }
}
