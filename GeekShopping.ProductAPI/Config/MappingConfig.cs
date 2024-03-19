using AutoMapper;
using GeekShopping.ProductAPI.Data.DTO_s;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration Register()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<Product, ProductDTO>();
            });
            return mapperConfig;
        }
    }
}
