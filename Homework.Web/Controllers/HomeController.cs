using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Homework.Web.Models;

namespace Homework.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        return View(await CreateProductModel());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<ProductModel> CreateProductModel()
    {
        HttpResponseMessage productApiJsonResponse = RequestJsonResponseMessage("https://dummyjson.com/products?limit=0");
        var productModel = new ProductModel();

        if (productApiJsonResponse.IsSuccessStatusCode)
        {
            var responseStream = await productApiJsonResponse.Content.ReadAsStringAsync();
            productModel = JsonConvert.DeserializeObject<ProductModel>(responseStream);
        }

        return productModel;
    }

    private HttpResponseMessage RequestJsonResponseMessage(string endpoint)
    {
        var message = new HttpRequestMessage();
        message.Method = HttpMethod.Get;
        message.RequestUri = new Uri(endpoint);
        message.Headers.Add("Accept", "application/json");
        var httpClient = _httpClientFactory.CreateClient();

        return httpClient.SendAsync(message).Result;
    }
}
