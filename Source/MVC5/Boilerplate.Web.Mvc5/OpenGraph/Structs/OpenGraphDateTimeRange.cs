namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;

    public class OpenGraphDateTimeRange
    {
        private readonly DateTime end;
        private readonly DateTime start;

        public OpenGraphDateTimeRange(DateTime start, DateTime end)
        {
            this.start = start;
            this.end = end;
        }

        public DateTime End { get { return this.end; } }

        public DateTime Start { get { return this.start; } }
    }
}
