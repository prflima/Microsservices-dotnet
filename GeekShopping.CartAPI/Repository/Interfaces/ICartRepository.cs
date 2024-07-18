using GeekShopping.CartAPI.Data;

namespace GeekShopping.CartAPI.Repository.Interfaces;

public interface ICartRepository
{
    Task<CartDto> FindCartByUserId(string userId);
    Task<CartDto> SaveOrUpdate(CartDto cartDto);
    Task<bool> RemoveFromCart(long cartDetailsId);
    Task<bool> ApplyCoupon(string userId, string couponCode);
    Task<bool> RemoveCoupon(string userId);
    Task<bool> ClearCart(string userId);
}
