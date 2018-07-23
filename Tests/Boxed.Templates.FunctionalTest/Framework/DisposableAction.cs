namespace Boxed.Templates.FunctionalTest
{
    using System;

    public class DisposableAction : IDisposable
    {
        private readonly Action action;

        public DisposableAction(Action action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        public void Dispose() => this.action();
    }
}
