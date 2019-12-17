namespace ApiTemplate.IntegrationTest.Fixtures
{
    using System;
    using System.Globalization;
    using Serilog.Core;
    using Serilog.Events;
    using Xunit.Abstractions;

    public class TestOutputSink : ILogEventSink
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TestOutputSink(ITestOutputHelper testOutputHelper) =>
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));

        public void Emit(LogEvent logEvent)
        {
            if (logEvent is null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            var shortLogLevel = logEvent.Level.ToString().Substring(0, 3).ToUpperInvariant();
            var renderedMessage = logEvent.MessageTemplate.Render(logEvent.Properties, CultureInfo.InvariantCulture);
            var message = FormattableString.Invariant($"[{logEvent.Timestamp:HH:mm:ss} {shortLogLevel}] {renderedMessage}");
            if (logEvent.Exception != null)
            {
                message += FormattableString.Invariant($"{Environment.NewLine}    {logEvent.Exception}");
            }

            foreach (var property in logEvent.Properties)
            {
                message += FormattableString.Invariant($"{Environment.NewLine}    {property.Key} = {property.Value}");
            }

            this.testOutputHelper.WriteLine(message);
        }
    }
}
