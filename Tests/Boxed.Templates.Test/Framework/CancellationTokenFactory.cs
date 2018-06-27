namespace Boxed.Templates.Test
{
    using System;
    using System.Threading;

    public static class CancellationTokenFactory
    {
        public static CancellationToken GetCancellationToken(TimeSpan? timeout) =>
            GetCancellationToken(timeout ?? TimeSpan.FromSeconds(30));

        public static CancellationToken GetCancellationToken(TimeSpan timeout) =>
            new CancellationTokenSource(timeout).Token;
    }
}
