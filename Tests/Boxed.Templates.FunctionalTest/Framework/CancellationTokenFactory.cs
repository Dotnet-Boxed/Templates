namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Threading;

    public static class CancellationTokenFactory
    {
        public static CancellationToken GetCancellationToken(TimeSpan? timeout) =>
            GetCancellationToken(timeout ?? TimeSpan.FromMinutes(1));

        public static CancellationToken GetCancellationToken(TimeSpan timeout) =>
            new CancellationTokenSource(timeout).Token;
    }
}
