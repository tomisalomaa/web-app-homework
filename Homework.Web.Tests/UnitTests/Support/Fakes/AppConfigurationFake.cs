using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Homework.Web.Tests.UnitTests.Support.Fakes
{
    public class AppConfigurationFake
    {
        public static IConfiguration CreateInMemoryCollection(Dictionary<string, string> appsettings)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appsettings)
                .Build();

            return configuration;
        }

        public IConfiguration CreateInMemoryProductEndpointConfiguration()
        {
            var inMemSettings = new Dictionary<string, string>
            {
                { "AppSettings:ProductApiEndpoint", "https://testendpoint.fake" }
            };

            return CreateInMemoryCollection(inMemSettings);
        }
    }
}
