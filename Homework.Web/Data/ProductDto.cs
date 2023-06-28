using Newtonsoft.Json;
using System.Xml.Linq;

namespace Homework.Web.Data
{
    public class ProductDto
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; } = new List<Product>();
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }

        public class Product
        {
            public int Id { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; } = "Unknown";
            [JsonProperty("description")]
            public string Description { get; set; } = "Unknown";
            public int Price { get; init; }
            public float DiscountPercentage { get; set; }
            public float Rating { get; set; }
            public int Stock { get; set; }
            [JsonProperty("brand")]
            public string Brand { get; set; } = "Unknown";
            [JsonProperty("category")]
            public string Category { get; set; } = "Unknown";
            [JsonProperty("thumbnail")]
            public string Thumbnail { get; set; } = "Unknown";
            [JsonProperty("images")]
            public List<string> Images { get; set; } = new List<string>();
        }
    }
}
