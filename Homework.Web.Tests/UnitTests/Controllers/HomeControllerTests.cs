namespace Homework.Web.Tests.UnitTests.Controllers;
using Homework.Web.Controllers;
using System.Net;
using Moq;
using Moq.Protected;
using Microsoft.Extensions.Logging;

public class HomeControllerTests
{
    private HomeController _sut;
    private readonly Mock<ILogger<HomeController>> _mockLogger;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<HomeController>>();
    }

    [SetUp]
    public void Setup()
    {
        // handler will always return 200 OK and no response body
        var mockHandler = new Mock<DelegatingHandler>();
        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK))
            .Verifiable();
        mockHandler.As<IDisposable>().Setup(s => s.Dispose());

        var httpClient = new HttpClient(mockHandler.Object);

        // create mock factory using the above client
        var mockFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);
        mockFactory
            .Setup(factory => factory.CreateClient(string.Empty))
            .Returns(httpClient)
            .Verifiable();

        _sut = new HomeController(_mockLogger.Object, mockFactory.Object);
    }
}