using Homework.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.Web.Tests.UnitTests.Support.Fake
{
    public class ProductDtoFake
    {
        public List<Product> Products { get; set; } = new List<Product>() { new Product() };
        public int Total { get; set; } = 1;
        public int Skip { get; set; } = 0;
        public int Limit { get; set; } = 0;

        public class Product
        {
            public int Id { get; set; } = 1;
            public string Title { get; set; } = "Fake phone product";
            public string Description { get; set; } = "This product is the latest fPhone";
            public int Price { get; set; } = 123;
            public float DiscountPercentage { get; set; } = 11.05F;
            public float Rating { get; set; } = 4.0F;
            public int Stock { get; set; } = 99;
            public string Brand { get; set; } = "fPhone";
            public string Category { get; set; } = "phone";
            public string Thumbnail { get; set; } = "https://fakeurl.thumbnail";
            public List<string> Images { get; set; } = new List<string>() { "https://fakeurl.image" };
        }
    }
}
