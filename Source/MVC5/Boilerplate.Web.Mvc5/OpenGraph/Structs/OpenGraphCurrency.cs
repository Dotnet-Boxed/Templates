namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;

    public class OpenGraphCurrency
    {
        private readonly double amount;
        private readonly string currency;

        public OpenGraphCurrency(double amount, string currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException("currency");
            }

            this.amount = amount;
            this.currency = currency;
        }

        public double Amount { get { return this.amount; } }

        public string Currency { get { return this.currency; } }
    }
}
