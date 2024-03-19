using GeekShopping.ProductAPI.Data.DTO_s;

namespace GeekShopping.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(long id);
        Task<ProductDTO> Create(ProductDTO product);
        Task<ProductDTO> Update(ProductDTO product);
        Task<bool> Delete(long id);
    }
}
