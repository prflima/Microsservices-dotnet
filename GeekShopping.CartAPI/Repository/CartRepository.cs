using AutoMapper;
using GeekShopping.CartAPI.Data;
using GeekShopping.CartAPI.Model;
using GeekShopping.CartAPI.Model.Context;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository;

public class CartRepository : ICartRepository
{
    private readonly MySQLContext _context;
    private IMapper _mapper;

    public CartRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCoupon(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ClearCart(string userId)
    {
        CartHeader? cartHeader = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.UserId == userId);

        if(cartHeader != null) 
        {
            _context.CartDetails
                .RemoveRange(_context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));

            _context.CartHeaders
                .Remove(cartHeader);

            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CartDto> FindCartByUserId(string userId)
    {
        var cartHeader = await _context.CartHeaders
            .FirstOrDefaultAsync(c => c.UserId == userId);

        var cartDetails = _context.CartDetails
            .Where(c => c.CartHeaderId == cartHeader!.Id)
            .Include(c => c.Product);

        return _mapper.Map<CartDto>(new CartDto
        {
            CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
            CartDetails = _mapper.Map<IEnumerable<CartDetailDto>>(cartDetails)
        });
    }

    public async Task<bool> RemoveCoupon(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveFromCart(long cartDetailsId)
    {
        try
        {
            CartDetail? cartDetail = await _context.CartDetails
                .FirstOrDefaultAsync(c => c.Id == cartDetailsId);

            if(!string.IsNullOrEmpty(cartDetail!.CartHeaderId.ToString()))
            {
                CartHeader? cartHeader = await _context.CartHeaders
                    .FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                _context.CartHeaders.Remove(cartHeader!);
            }

            _context.CartDetails.Remove(cartDetail);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<CartDto> SaveOrUpdate(CartDto cartDto)
    {
        Cart cart = _mapper.Map<Cart>(cartDto);

        // Checks if the product is already saved in the database if it does not exist then save.
        var product = await _context.Products.FirstOrDefaultAsync(
            p => p.Id == cartDto.CartDetails.FirstOrDefault().ProductId);

        if (product == null)
        {
            _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
            await _context.SaveChangesAsync();
        }

        // Check if CartHeader is null.
        var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
            c => c.UserId == cart.CartHeader!.UserId);

        if (cartHeader == null)
        {
            // Create CartHeader and CartDetails
            _context.CartHeaders.Add(cart.CartHeader!);
            await _context.SaveChangesAsync();
            cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader!.Id;
            cart.CartDetails.FirstOrDefault().Product = null;
            _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
            await _context.SaveChangesAsync();
        }
        else
        {
            // if CartHeader is not null, check if CartDetails has same product.
            var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                p.CartHeaderId == cartHeader.Id);

            if (cartDetail == null)
            {
                // Create CartDetails.
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                // If has a same product, update product count and cart details.
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
        }

        return _mapper.Map<CartDto>(cart);
    }
}
