namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;

    public class ConsoleColorScope : IDisposable
    {
        private readonly ConsoleColor backgroundColor;
        private readonly ConsoleColor foregroundColor;

        public ConsoleColorScope(ConsoleColor foregroundColor)
            : this(Console.BackgroundColor, foregroundColor)
        {
        }

        public ConsoleColorScope(
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor)
        {
            this.backgroundColor = Console.BackgroundColor;
            this.foregroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }

        public void Dispose()
        {
            Console.BackgroundColor = this.backgroundColor;
            Console.ForegroundColor = this.foregroundColor;
        }
    }
}
