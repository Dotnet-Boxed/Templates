namespace OrleansTemplate.Server
{
    using System;
    using Orleans.Runtime;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Adds a new Orleans Trace ID enricher to the log event.
    /// </summary>
    public class TraceIdEnricher : ILogEventEnricher
    {
        /// <summary>
        /// Enrich the log event.
        /// </summary>
        /// <param name="logEvent">The log event to enrich.</param>
        /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent is null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            if (propertyFactory is null)
            {
                throw new ArgumentNullException(nameof(propertyFactory));
            }

            var traceId = RequestContext.Get("TraceId");
            var property = propertyFactory.CreateProperty("TraceId", traceId);
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
