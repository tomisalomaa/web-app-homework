using Homework.Web.Data;
using Homework.Web.Services.Interfaces;
using Newtonsoft.Json;

namespace Homework.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _serviceEndpoint;

        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _serviceEndpoint = configuration.GetValue<string>("AppSettings:ProductApiEndpoint");
        }

        public async Task<ProductDto> GetAllProductsAsync()
        {
            ProductDto productDto = new();

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_serviceEndpoint}?limit=0")
                };
                message.Headers.Add("Accept", "application/json");
                var response = await httpClient.SendAsync(message);
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    productDto = JsonConvert.DeserializeObject<ProductDto>(content) ?? productDto;
                }
            }

            return productDto;
        }
    }
}
