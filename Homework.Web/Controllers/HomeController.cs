using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Homework.Web.Models;
using Homework.Web.Services;
using Homework.Web.Services.Interfaces;

namespace Homework.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;

    public HomeController(ILogger<HomeController> logger, IProductService productService)
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
            var content = await productApiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ProductModel>(content);
        }

        return new ProductModel();
    }
}
