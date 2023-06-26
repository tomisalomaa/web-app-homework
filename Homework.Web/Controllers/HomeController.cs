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

    public HomeController(ILogger<HomeController> logger, ProductService productService)
    {
        _logger = logger;
        _productService = productService;
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
        var productApiResponse = await _productService.GetAllProducts();

        if (productApiResponse.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<ProductModel>(await productApiResponse.Content.ReadAsStringAsync());
        }

        return new ProductModel();
    }
}
