using System.Net.Http;

namespace Homework.Web.Services
{
    public class ProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serviceEndpoint;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serviceEndpoint = "https://dummyjson.com/products";
        }

        public HttpResponseMessage GetAllProductsJson()
        {
            var httpClient = CreateClient();
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_serviceEndpoint}?limit=0");
            message.Headers.Add("Accept", "application/json");

            return httpClient.SendAsync(message).Result;
        }

        public HttpResponseMessage GetProductCategoriesJson()
        {
            var httpClient = CreateClient();
            var message = new HttpRequestMessage();
            message.Method = HttpMethod.Get;
            message.RequestUri = new Uri($"{_serviceEndpoint}/categories");
            message.Headers.Add("Accept", "application/json");

            return httpClient.SendAsync(message).Result;
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient();
        }
    }
}
