using Homework.Web.Controllers;
using Homework.Web.Data;
using Homework.Web.Models;
using Homework.Web.Services;
using Homework.Web.Services.Interfaces;
using Homework.Web.Tests.UnitTests.Support.Fake;
using Homework.Web.Tests.UnitTests.Support.Fakes;
using Homework.Web.Tests.UnitTests.Support.Stubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Xml.Serialization;

namespace Homework.Web.Tests.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly HttpResponseMessage _fakeHttpResponseOk;

        public ProductServiceTests()
        {
            var fakeConfiguration = new AppConfigurationFake();
            _configuration = fakeConfiguration.CreateInMemoryProductEndpointConfiguration();
            _fakeHttpResponseOk = HttpResponseStub.SingleProductResponseOk();
            _fakeHttpResponseOk.Content = new StringContent(JsonConvert.SerializeObject(new ProductDtoFake()));
        }

        [Test]
        public async Task GetAllProductsAsync_ResponseStatusOk_ReturnsProductDtoWithContent()
        {
            var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(_fakeHttpResponseOk);
            var mockHttpClient = new Mock<HttpClient>(mockHttpMessageHandler.Object);
            var mockHttpClientFactory = HttpResponseStub.CreateMockClientFactory(mockHttpClient.Object);
            var sut = new ProductService(mockHttpClientFactory.Object, _configuration);

            var result = await sut.GetAllProductsAsync();

            Assert.That(result, Is.InstanceOf<ProductDto>());
            Assert.That(result.Products, Has.Count.GreaterThan(0));
            Assert.That(result.Products.First().Title, Is.Not.Null.And.Not.Empty);
        }

        [TestCaseSource(nameof(ErrorResponseCases))]
        public async Task GetAllProductsAsync_ResponseStatusServerError_ReturnsProductDtoWithoutProductContent(HttpResponseMessage response, string content)
        {
            response.Content = new StringContent(content);
            var mockHttpMessageHandler = HttpResponseStub.MockMessageHandlerWithResponse(response);
            var mockHttpClient = new Mock<HttpClient>(mockHttpMessageHandler.Object);
            var mockHttpClientFactory = HttpResponseStub.CreateMockClientFactory(mockHttpClient.Object);
            var sut = new ProductService(mockHttpClientFactory.Object, _configuration);

            var result = await sut.GetAllProductsAsync();

            Assert.That(result, Is.InstanceOf<ProductDto>());
            Assert.That(result.Products, Has.Count.EqualTo(0));
        }

        public static IEnumerable<TestCaseData> ErrorResponseCases
        {
            get
            {
                yield return new TestCaseData(HttpResponseStub.ClientErrorResponse(), "Bad Request");
                yield return new TestCaseData(HttpResponseStub.ServerErrorResponse(), "Internal Server Error");
            }
        }
    }
}
