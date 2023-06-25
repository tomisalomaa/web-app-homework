using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Homework.Web.Models;
using Homework.Web.Services;

namespace Homework.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ProductService _productService;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _productService = new ProductService(httpClientFactory);
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
        HttpResponseMessage productApiJsonResponse = _productService.GetAllProductsJson();
        var productModel = new ProductModel();

        if (productApiJsonResponse.IsSuccessStatusCode)
        {
            var responseStream = await productApiJsonResponse.Content.ReadAsStringAsync();
            productModel = JsonConvert.DeserializeObject<ProductModel>(responseStream);
        }

        return productModel;
    }
}
