namespace Homework.Web.Tests.UnitTests.Controllers;
using Homework.Web.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Homework.Web.Services;
using Homework.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Homework.Web.Tests.UnitTests.Support.Stubs;
using Microsoft.Extensions.Configuration;
using Homework.Web.Tests.UnitTests.Support.Fakes;

public class HomeControllerTests
{
    private Mock<ILogger<HomeController>> _mockLogger;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<HomeController>>();
    }

    [Test]
    public async Task Index_ModelPopulatedSuccessfully_ViewReturnsCorrectView()
    {
        var mockResponse = HttpResponseStub.SingleProductResponseOk();
        var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(mockResponse);
        var mockClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockClientFactory = HttpResponseStub.CreateMockClientFactory(mockClient);
        var fakeConfiguration = new AppConfigurationFake();
        IConfiguration configuration = fakeConfiguration.CreateInMemoryProductEndpointConfiguration();

        var sut = new HomeController(_mockLogger.Object, new ProductService(mockClientFactory.Object, configuration));

        var viewResult = await sut.Index() as ViewResult;

        Assert.That(viewResult.Model, Is.InstanceOf<ProductModel>());
    }

    [Test]
    public async Task Index_ClientErrorWhilePopulatingModel_ViewReturnsCorrectView()
    {
        var mockResponse = HttpResponseStub.ClientErrorResponse();
        var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(mockResponse);
        var mockClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockClientFactory = HttpResponseStub.CreateMockClientFactory(mockClient);
        var fakeConfiguration = new AppConfigurationFake();
        IConfiguration configuration = fakeConfiguration.CreateInMemoryProductEndpointConfiguration();

        var sut = new HomeController(_mockLogger.Object, new ProductService(mockClientFactory.Object, configuration));

        var viewResult = await sut.Index() as ViewResult;

        Assert.That(viewResult.Model, Is.InstanceOf<ProductModel>());
    }

    [Test]
    public async Task Index_ServerErrorWhilePopulatingModel_ViewReturnsCorrectView()
    {
        var mockResponse = HttpResponseStub.ServerErrorResponse();
        var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(mockResponse);
        var mockClient = new HttpClient(mockHttpMessageHandler.Object);
        var mockClientFactory = HttpResponseStub.CreateMockClientFactory(mockClient);
        var fakeConfiguration = new AppConfigurationFake();
        IConfiguration configuration = fakeConfiguration.CreateInMemoryProductEndpointConfiguration();

        var sut = new HomeController(_mockLogger.Object, new ProductService(mockClientFactory.Object, configuration));

        var viewResult = await sut.Index() as ViewResult;

        Assert.That(viewResult.Model, Is.InstanceOf<ProductModel>());
    }
}