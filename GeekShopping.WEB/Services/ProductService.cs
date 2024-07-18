using GeekShopping.WEB.Models;
using GeekShopping.WEB.Services.IServices;
using GeekShopping.WEB.Utils;
using System.Net.Http.Headers;

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

        public async Task<IEnumerable<ProductModel>> GetProducts(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> GetById(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Create(ProductModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong when calling ProductAPI");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<ProductModel> Update(ProductModel model, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsJson(BasePath, model);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong when calling ProductAPI");

            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<bool> Delete(long id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{BasePath}/{id}");
            if (response.IsSuccessStatusCode)
                return true;
            else throw new Exception("Something went wrong when calling API");
        }
    }
}
