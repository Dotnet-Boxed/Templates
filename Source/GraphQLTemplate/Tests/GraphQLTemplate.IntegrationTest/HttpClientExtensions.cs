namespace GraphQLTemplate.IntegrationTest
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public static class HttpClientExtensions
    {
        private const string GraphQLContentType = "application/graphql";

        public static Task<HttpResponseMessage> PostGraphQLAsync(this HttpClient httpClient, string content)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (content.Length == 0)
            {
                throw new ArgumentException("Cannot be empty.", nameof(content));
            }

            using var stringContent = new StringContent(content, Encoding.UTF8, GraphQLContentType);
            return httpClient.PostAsync(new Uri("/graphql", UriKind.Relative), stringContent);
        }
    }
}
