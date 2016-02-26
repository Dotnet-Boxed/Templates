namespace Boilerplate.Web.Mvc.TagHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Razor.TagHelpers;
    using Microsoft.Extensions.Caching.Distributed;

    /// <summary>
    /// Adds Subresource Integrity (SRI) to a script tag. Subresource Integrity (SRI) is a security feature that 
    /// enables browsers to verify that files they fetch (for example, from a CDN) are delivered without unexpected 
    /// manipulation. It works by allowing you to provide a cryptographic hash that a fetched file must match.
    /// See https://developer.mozilla.org/en-US/docs/Web/Security/Subresource_Integrity and https://www.w3.org/TR/SRI/.
    /// The tag helper works by taking the URL from the script tag and checking the <see cref="IDistributedCache"/> for 
    /// a matching SRI value. If one is found, it is used otherwise a HTTP request is made to the URL, the SRI is
    /// calculated and the SRI is stored in the <see cref="IDistributedCache"/>.
    /// </summary>
    public abstract class SubresourceIntegrityTagHelper : TagHelper
    {
        protected const string SubresourceIntegrityAttributeName = "asp-subresource-integrity";
        private const string CrossOriginAttributeName = "crossorigin";
        private const string IntegrityAttributeName = "integrity";

        private readonly IDistributedCache distributedCache;
        private readonly HttpClient httpClient;

        public SubresourceIntegrityTagHelper(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;

            var httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            };
            this.httpClient = new HttpClient(httpClientHandler);
        }

        /// <summary>
        /// Gets or sets the hashing algorithm. This is a required property.
        /// </summary>
        [HtmlAttributeName(SubresourceIntegrityAttributeName)]
        public SubresourceIntegrityHashAlgorithm HashAlgorithms { get; set; } = SubresourceIntegrityHashAlgorithm.SHA256;

        /// <summary>
        /// Gets the name of the attribute which contains the URL to the resource.
        /// </summary>
        /// <value>
        /// The name of the attribute containing the URL to the resource.
        /// </value>
        protected abstract string UrlAttributeName { get; }

        /// <summary>
        /// Releases managed resources.
        /// </summary>
        public void Dispose()
        {
            this.httpClient.Dispose();
        }

        /// <summary>
        /// Asynchronously executes the <see cref="TagHelper" /> with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        /// <returns>A task representing the operation.</returns>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var url = output.Attributes[this.UrlAttributeName].Value.ToString();

            if (url.StartsWith("//"))
            {
                url = "https:" + url;
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                var sri = await this.GetCachedSri(url);
                if (sri == null)
                {
                    sri = await this.GetSubresourceIntegrity(url, this.HashAlgorithms);
                    await this.SetCachedSri(url, sri);
                }

                output.Attributes[CrossOriginAttributeName] = "anonymous";
                output.Attributes[IntegrityAttributeName] = sri;
            }
        }

        /// <summary>
        /// Gets the key used to retrieve the SRI value from the distributed cache.
        /// </summary>
        /// <param name="url">The URL to the resource.</param>
        /// <returns>A key value for the URL.</returns>
        protected virtual string GetSriKey(string url)
        {
            return "SRI:" + url;
        }

        #region Private Static Methods

        private static IEnumerable<Enum> GetFlags(Enum enumeration)
        {
            foreach (Enum value in Enum.GetValues(enumeration.GetType()))
            {
                if (enumeration.HasFlag(value))
                {
                    yield return value;
                }
            }
        }

        private static HashAlgorithm CreateHashAlgorithm<T>()
            where T : HashAlgorithm
        {
            var type = typeof(T);
            if (type == typeof(SHA256))
            {
                return SHA256.Create();
            }
            else if (type == typeof(SHA384))
            {
                return SHA384.Create();
            }
            else if (type == typeof(SHA512))
            {
                return SHA512.Create();
            }
            else
            {
                throw new ArgumentException(
                    $"Hash algorithm not recognized. Type<{type}>.",
                    nameof(T));
            }
        }

        private static string GetHash<T>(byte[] bytes)
            where T : HashAlgorithm
        {
            using (var hashAlgorithm = CreateHashAlgorithm<T>())
            {
                var hashedBytes = hashAlgorithm.ComputeHash(bytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private static string GetSri(byte[] bytes, SubresourceIntegrityHashAlgorithm hashAlgorithm)
        {
            switch (hashAlgorithm)
            {
                case SubresourceIntegrityHashAlgorithm.SHA256:
                    return "sha256-" + GetHash<SHA256>(bytes);
                case SubresourceIntegrityHashAlgorithm.SHA384:
                    return "sha384-" + GetHash<SHA384>(bytes);
                case SubresourceIntegrityHashAlgorithm.SHA512:
                    return "sha512-" + GetHash<SHA512>(bytes);
                default:
                    throw new ArgumentException(
                        $"Hash algorithm not recognized. HashAlgorithm<{hashAlgorithm}>.",
                        nameof(hashAlgorithm));
            }
        }

        #endregion


        #region Private Methods

        private async Task<string> GetCachedSri(string url)
        {
            var key = GetSriKey(url);
            var value = await this.distributedCache.GetAsync(key);
            return value == null ? null : Encoding.UTF8.GetString(value);
        }

        private async Task SetCachedSri(string url, string value)
        {
            var key = GetSriKey(url);
            await this.distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(value));
        }

        private async Task<string> GetSubresourceIntegrity(
            string url,
            SubresourceIntegrityHashAlgorithm hashAlgorithms)
        {
            var bytes = await this.httpClient.GetByteArrayAsync(url);

            List<string> items = new List<string>(3);
            foreach (SubresourceIntegrityHashAlgorithm hashAlgorithm in GetFlags(hashAlgorithms))
            {
                items.Add(GetSri(bytes, hashAlgorithm));
            }

            return string.Join(" ", items);
        }

        #endregion
    }
}
