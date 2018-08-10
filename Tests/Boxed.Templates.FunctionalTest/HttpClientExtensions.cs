namespace Boxed.Templates.FunctionalTest
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public static class HttpClientExtensions
    {
        private const string GraphQLContentType = "application/graphql";

        public static Task<HttpResponseMessage> PostGraphQL(this HttpClient httpClient, string content)
        {
            var stringContent = new StringContent(content, Encoding.UTF8, GraphQLContentType);
            return httpClient.PostAsync("/graphql", stringContent);
        }
    }
}
