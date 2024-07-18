using AutoMapper;
using GeekShopping.CartAPI.Data;
using GeekShopping.CartAPI.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration Register()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
                config.CreateMap<CartDto, Cart>().ReverseMap();
            });
            return mapperConfig;
        }
    }
}
