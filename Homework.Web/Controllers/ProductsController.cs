using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Homework.Web.Models;
using Homework.Web.Services;
using Homework.Web.Services.Interfaces;
using Homework.Web.Data;

namespace Homework.Web.Controllers;

public class ProductsController : Controller
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(ILogger<ProductsController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var productDto = await _productService.GetAllProductsAsync();
            var viewModel = new ProductsViewModel()
            {
                TrendingProduct = productDto.Products
                .Where(product => product.DiscountPercentage >= 10)
                .OrderByDescending(product => product.Rating)
                .Select(product => product.Title)
                .FirstOrDefault()
            };

            foreach (var item in productDto.Products.Where(product => product.DiscountPercentage >= 10))
            {
                viewModel.AddProductToList(item.Title, item.Price, item.DiscountPercentage, item.Rating, item.Brand);
            }

            return View(viewModel);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error when mapping products to view");
            return View(new ProductsViewModel());
        }
        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
