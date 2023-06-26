using Homework.Web.Services;
using Homework.Web.Tests.UnitTests.Support.Fake;
using Homework.Web.Tests.UnitTests.Support.Stubs;
using Moq;
using Newtonsoft.Json;

namespace Homework.Web.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {
        [Test]
        public async Task GetAllProductsJson_ResponseStatusOk_ReturnsExpectedJsonResponse()
        {
            var fakeProducts = new ProductsFake()
            {
                Products = new List<ProductFake> { new ProductFake() }
            };
            var expectedResponse = JsonConvert.SerializeObject(fakeProducts);
            var mockResponse = HttpResponse.SingleProductResponseOk();
            var mockHttpMessageHandler = HttpResponse.MockMessageHandlerWithResponse(mockResponse);
            var mockClient = new HttpClient(mockHttpMessageHandler.Object);
            var mockClientFactory = new Mock<IHttpClientFactory>();
            mockClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(mockClient);

            var sut = new ProductService(mockClientFactory.Object);

            var resultJson = await sut.GetAllProducts().GetAwaiter().GetResult().Content.ReadAsStringAsync();

            Assert.That(resultJson, Is.EqualTo(expectedResponse));
        }
    }
}
