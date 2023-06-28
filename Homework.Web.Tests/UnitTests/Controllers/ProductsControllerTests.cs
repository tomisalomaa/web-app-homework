namespace Homework.Web.Tests.UnitTests.Controllers;
using Homework.Web.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Homework.Web.Tests.UnitTests.Support.Fakes;
using Homework.Web.Services.Interfaces;
using Homework.Web.Tests.UnitTests.Support.Fake;
using Newtonsoft.Json;
using Homework.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Homework.Web.Models;

public class HomeControllerTests
{
    private readonly Mock<ILogger<ProductsController>> _mockLogger;
    private ProductsController _sut;
    private Mock<IProductService> _mockProductService;
    private Mock<ProductDto> _mockProductDto;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<ProductsController>>();
    }

    [SetUp]
    public void Setup()
    {
        _mockProductDto = new Mock<ProductDto>();
        _mockProductService = new Mock<IProductService>();
        _mockProductService
            .Setup(service => service.GetAllProductsAsync())
            .ReturnsAsync(_mockProductDto.Object);
        _sut = new ProductsController(_mockLogger.Object, _mockProductService.Object);
    }

    [Test]
    public async Task Index_WhenNoException_ReturnsViewWithProductsViewModel()
    {
        var result = await _sut.Index() as ViewResult;

        Assert.That(result!.Model, Is.InstanceOf<ProductsViewModel>());
    }

    [Test]
    public async Task Index_WhenNoException_MapsHighestTrendingProduct()
    {
        var expectedTrendingProduct = "Highest Product";
        _mockProductDto.Object.Products = new List<ProductDto.Product>()
        {
            new ProductDto.Product()
            {
            Id = 1,
            Title = "Lowest Product",
            DiscountPercentage = 11.5f,
            Rating = 1
            },
            new ProductDto.Product()
            {
            Id = 2,
            Title = expectedTrendingProduct,
            DiscountPercentage = 11.5f,
            Rating = 3
            },
        };

        var result = await _sut.Index() as ViewResult;
        var viewResultModel = result!.Model as ProductsViewModel;

        Assert.That(viewResultModel!.TrendingProduct, Is.EqualTo(expectedTrendingProduct));
    }

    [Test]
    public async Task Index_WhenNoException_MapsProductsWithDiscountAtLeastTenPercent()
    {

        _mockProductDto.Object.Products = new List<ProductDto.Product>()
        {
            new ProductDto.Product()
            {
            Id = 1,
            Title = "Fake Product",
            DiscountPercentage = 9.9f
            },
            new ProductDto.Product()
            {
            Id = 1,
            Title = "Fake Product",
            DiscountPercentage = 10f
            },
            new ProductDto.Product()
            {
            Id = 1,
            Title = "Fake Product",
            DiscountPercentage = 10.1f
            },
        };

        var result = await _sut.Index() as ViewResult;
        var viewResultModel = result!.Model as ProductsViewModel;

        Assert.That(viewResultModel!.Products, Has.Count.EqualTo(2));
    }
}
