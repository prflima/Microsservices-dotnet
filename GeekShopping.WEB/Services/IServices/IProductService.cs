using GeekShopping.WEB.Models;

namespace GeekShopping.WEB.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProducts(string token);
        Task<ProductModel> GetById(long id, string token);
        Task<ProductModel> Create(ProductModel model, string token);
        Task<ProductModel> Update(ProductModel model, string token);
        Task<bool> Delete(long id, string token);
    }
}
