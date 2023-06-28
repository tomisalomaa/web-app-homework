namespace Homework.Web.Models
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public string? TrendingProduct { get; set; }

        public class Product
        {
            public string Title { get; set; } = string.Empty;
            public int Price { get; set; }
            public float DiscountPercentage { get; set; }
            public float Rating { get; set; }
            public string Brand { get; set; } = string.Empty;
            public string Currency { get; set; } = "€";
        }

        public void AddProductToList(string title, int price, float discount, float rating, string brand)
        {
            Products.Add(new Product() 
            { 
                Title = title,
                Price = price,
                DiscountPercentage = discount,
                Rating = rating,
                Brand = brand
            });
        }
    }
}
