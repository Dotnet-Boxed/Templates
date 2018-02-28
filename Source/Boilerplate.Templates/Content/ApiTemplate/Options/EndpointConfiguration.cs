namespace ApiTemplate.Options
{
    /// <summary>
    /// This class was taken from the link below and can be removed when upgrading to ASP.NET Core 2.1.
    /// See https://blogs.msdn.microsoft.com/webdev/2017/11/29/configuring-https-in-asp-net-core-across-different-platforms/
    /// </summary>
    public class EndpointConfiguration
    {
        public string Host { get; set; }

        public int? Port { get; set; }

        public string Scheme { get; set; }

        public string StoreName { get; set; }

        public string StoreLocation { get; set; }

        public string FilePath { get; set; }

        public string Password { get; set; }
    }
}
