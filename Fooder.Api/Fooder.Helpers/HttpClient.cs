using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace Fooder.Helpers
{
    public sealed class HttpClient
    {
        private readonly string _baseUrl;

        public HttpClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<IRestResponse> GetAsync(string resource, ICollection<KeyValuePair<string, string>> headers = null)
        {
            var route = GetRoute(resource);
            var client = new RestClient(route);

            var headersCollection = headers
                ?? Enumerable.Empty<KeyValuePair<string, string>>()
                    .ToList();

            var request = new RestRequest(Method.GET).AddHeaders(headersCollection);

            var response = await client.ExecuteAsync(request);

            return response;
        }

        public async Task<IRestResponse> PostAsync(
            string resource,
            string jsonBody,
            ICollection<KeyValuePair<string, string>> headers = null)
        {
            var route = GetRoute(resource);
            var client = new RestClient(route);

            var headersCollection = headers
                ?? Enumerable.Empty<KeyValuePair<string, string>>()
                    .ToList();

            var request = new RestRequest(Method.POST).AddHeaders(headersCollection)
                .AddJsonBody(jsonBody);

            var response = await client.ExecuteAsync(request);

            return response;
        }

        private string GetRoute(string resource) =>
            $"{_baseUrl}{resource}";
    }
}
