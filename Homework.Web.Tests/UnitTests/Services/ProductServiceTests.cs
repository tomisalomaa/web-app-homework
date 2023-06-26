using Homework.Web.Services;
using Homework.Web.Tests.UnitTests.Support.Fake;
using Homework.Web.Tests.UnitTests.Support.Fakes;
using Homework.Web.Tests.UnitTests.Support.Stubs;
using Microsoft.Extensions.Configuration;
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
            var mockResponse = HttpResponseStub.SingleProductResponseOk();
            var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(mockResponse);
            var mockClient = new HttpClient(mockHttpMessageHandler.Object);
            var mockClientFactory = new Mock<IHttpClientFactory>();
            mockClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(mockClient);
            var fakeConfiguration = new AppConfigurationFake();
            IConfiguration configuration = fakeConfiguration.CreateInMemoryProductEndpointConfiguration();

            var sut = new ProductService(mockClientFactory.Object, configuration);

            var resultJson = await sut.GetAllProducts().GetAwaiter().GetResult().Content.ReadAsStringAsync();

            Assert.That(resultJson, Is.EqualTo(expectedResponse));
        }
    }
}
