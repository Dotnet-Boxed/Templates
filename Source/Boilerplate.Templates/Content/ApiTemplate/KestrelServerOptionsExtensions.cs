namespace ApiTemplate
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using ApiTemplate.Options;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This class was taken from the ASP.NET Core blog post below but tweaked to match what is coming in ASP.NET Core 2.1.
    /// See https://blogs.msdn.microsoft.com/webdev/2017/11/29/configuring-https-in-asp-net-core-across-different-platforms/
    /// </summary>
    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureEndpoints(this KestrelServerOptions options)
        {
            var kestrelOptions = options.ApplicationServices.GetRequiredService<KestrelOptions>();
            foreach (var keyValuePair in kestrelOptions.Endpoints)
            {
                var endpointOptions = keyValuePair.Value;
                var url = endpointOptions.Url;

                var ipAddresses = new List<IPAddress>();
                if (url.Host == "localhost")
                {
                    ipAddresses.Add(IPAddress.IPv6Loopback);
                    ipAddresses.Add(IPAddress.Loopback);
                }
                else if (IPAddress.TryParse(url.Host, out var address))
                {
                    ipAddresses.Add(address);
                }
                else
                {
                    ipAddresses.Add(IPAddress.IPv6Any);
                }

                foreach (var address in ipAddresses)
                {
                    options.Listen(
                        address,
                        url.Port,
                        listenOptions =>
                        {
                            if (url.Scheme == Uri.UriSchemeHttps)
                            {
                                var certificate = LoadCertificate(endpointOptions.Certificate);
                                listenOptions.UseHttps(certificate);
                            }
                        });
                }
            }
        }

        private static X509Certificate2 LoadCertificate(CertificateOptions options)
        {
            if (options.Store != null && options.Location != null)
            {
                using (var store = new X509Store(options.Store, Enum.Parse<StoreLocation>(options.Location)))
                {
                    store.Open(OpenFlags.ReadOnly);
                    var certificate = store.Certificates.Find(
                        X509FindType.FindBySubjectName,
                        options.Subject,
                        validOnly: !options.AllowInvalid);

                    if (certificate.Count == 0)
                    {
                        throw new InvalidOperationException($"Certificate not found for {options.Subject}.");
                    }

                    return certificate[0];
                }
            }

            if (options.Path != null && options.Password != null)
            {
                return new X509Certificate2(options.Path, options.Password);
            }

            throw new InvalidOperationException("No valid certificate configuration found for the current endpoint.");
        }
    }
}
