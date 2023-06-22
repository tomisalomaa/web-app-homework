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
        return View(await PopulateProductModel());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<ProductModel> PopulateProductModel()
    {
        var message = CreateJsonRequestMessage("https://dummyjson.com/products?limit=0");
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.SendAsync(message);
        var productModel = new ProductModel();

        // This I would like to see extracted somehow for easier unit testing.
        // Also, should we try-catch? Should probably consider exception for this call in some way.
        // Not sure if else returning a null Product model is ideal either, since how do we separate
        // non-success codes from success codes without debugging?
        // TODO: potential refactor
        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            productModel = JsonConvert.DeserializeObject<ProductModel>(responseStream);
        }

        return productModel;
    }

    private HttpRequestMessage CreateJsonRequestMessage(string url)
    {
        var message = new HttpRequestMessage();
        message.Method = HttpMethod.Get;
        message.RequestUri = new Uri(url);
        message.Headers.Add("Accept", "application/json");

        return message;
    }
}
