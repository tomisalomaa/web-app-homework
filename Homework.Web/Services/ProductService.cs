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

        public async Task<HttpResponseMessage> GetAllProducts()
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_serviceEndpoint}?limit=0")
                };
                message.Headers.Add("Accept", "application/json");

                return await httpClient.SendAsync(message);
            }
        }
    }
}
