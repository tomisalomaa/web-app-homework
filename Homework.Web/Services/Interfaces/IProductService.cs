namespace Homework.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<HttpResponseMessage> GetAllProducts();
    }
}
