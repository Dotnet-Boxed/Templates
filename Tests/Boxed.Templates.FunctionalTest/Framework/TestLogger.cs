namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Diagnostics;

    public static class TestLogger
    {
        public static void Write(string message, ConsoleColor? color = null) =>
            UseColor(
                () =>
                {
                    Debug.Write(message);
                    Console.Write(message);
                },
                color);

        public static void WriteLine()
        {
            Debug.WriteLine(string.Empty);
            Console.WriteLine();
        }

        public static void WriteLine(string message, ConsoleColor? color = null) =>
            UseColor(
                () =>
                {
                    Debug.WriteLine(message);
                    Console.WriteLine(message);
                },
                color);

        private static void UseColor(Action action, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                using (new ConsoleColorScope(color.Value))
                {
                    action();
                }
            }
            else
            {
                action();
            }
        }
    }
}
