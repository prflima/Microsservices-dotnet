using GeekShopping.WEB.Models;
using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Utils;

namespace GeekShopping.WEB.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public const string BasePath = "api/v1/product";

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetById(long id)
        {
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel model)
        {
            var response = await _httpClient.PostAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong when calling ProductAPI");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Update(ProductModel model)
        {
            var response = await _httpClient.PutAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong when calling ProductAPI");

            return await response.ReadContentAs<ProductModel>();
        }

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
