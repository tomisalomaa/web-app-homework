using Homework.Web.Data;

namespace Homework.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetAllProductsAsync();
    }
}
