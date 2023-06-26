using Homework.Web.Tests.UnitTests.Support.Fake;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace Homework.Web.Tests.UnitTests.Support.Stubs
{
    public static class HttpResponse
    {
        public static HttpResponseMessage SingleProductResponseOk()
        {
            var fakeProducts = new ProductsFake();
            fakeProducts.Products.Add(new ProductFake());
            var message = CreateBaseRequestMessage();

            return new HttpResponseMessage()
            {
                RequestMessage = message,
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "OK",
                Content = new StringContent(JsonConvert.SerializeObject(fakeProducts))
            };
        }

        public static HttpResponseMessage ClientErrorResponse()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "Bad Request"
            };
        }

        public static HttpResponseMessage ServerErrorResponse()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = "Internal Server Error"
            };
        }

        public static Mock<HttpMessageHandler> MockMessageHandlerWithResponse(HttpResponseMessage mockResponse)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponse);

            return mockHttpMessageHandler;
        }

        private static HttpRequestMessage CreateBaseRequestMessage()
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://fakeproduct.response")
            };
        }
    }
}
