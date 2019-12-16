namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public static class HttpClientExtensions
    {
        private const string GraphQLContentType = "application/graphql";

        public static async Task<HttpResponseMessage> PostGraphQLAsync(this HttpClient httpClient, string content)
        {
            if (httpClient is null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            using (var stringContent = new StringContent(content, Encoding.UTF8, GraphQLContentType))
            {
                return await httpClient
                    .PostAsync(new Uri("/graphql", UriKind.Relative), stringContent)
                    .ConfigureAwait(false);
            }
        }
    }
}
