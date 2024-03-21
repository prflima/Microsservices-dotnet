using GeekShopping.WEB.Models;

namespace GeekShopping.WEB.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProducts();
        Task<ProductModel> GetById(long id);
        Task<ProductModel> Create(ProductModel model);
        Task<ProductModel> Update(ProductModel model);
        Task<bool> Delete(long id);
    }
}
