using AutoMapper;
using GeekShopping.ProductAPI.Data.DTO_s;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper, MySQLContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(long id)
        {
            var product = await GetProductById(id);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Create(ProductDTO product)
        {
            var entity = _mapper.Map<Product>(product);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ProductDTO> Update(ProductDTO product)
        {
            var entity = _mapper.Map<Product>(product);
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await GetProductById(id);

                if(product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<Product> GetProductById(long id)
        {
            var product = await  _context.Products
                                 .Where(p => p.Id == id)
                                 .FirstOrDefaultAsync();

            return product;
        }
    }
}
