namespace Boxed.Templates.Test
{
    using System;
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;

    public abstract class HttpClientTest : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly HttpClientHandler httpClientHandler;

        public HttpClientTest()
        {
            this.httpClientHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = this.ValidateCertificate,
            };
            this.httpClient = new HttpClient(this.httpClientHandler);
        }

        public HttpClient HttpClient => this.httpClient;

        public void Dispose()
        {
            this.httpClient.Dispose();
            this.httpClientHandler.Dispose();
        }

        public virtual bool ValidateCertificate(
            HttpRequestMessage request,
            X509Certificate2 certificate,
            X509Chain chain,
            SslPolicyErrors errors) =>
            string.Equals(certificate.Subject, "CN=localhost", StringComparison.Ordinal);
    }
}