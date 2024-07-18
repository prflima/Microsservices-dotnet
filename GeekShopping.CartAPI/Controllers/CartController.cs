using GeekShopping.CartAPI.Data;
using GeekShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new
                ArgumentNullException(nameof(cartRepository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDto>> FindByUserId([FromRoute] string userId)
        {
            var cart = await _cartRepository.FindCartByUserId(userId);
            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> AddCart([FromBody] CartDto cartDto)
        {
            if (cartDto == null) return BadRequest();

            var cart = await _cartRepository.SaveOrUpdate(cartDto);
            if (cart == null) return BadRequest();

            return Ok(cart);
        }

        [HttpPut]
        public async Task<ActionResult<CartDto>> UpdateCart([FromBody] CartDto cartDto)
        {
            if(cartDto  == null) return BadRequest();

            var cart = await _cartRepository.SaveOrUpdate(cartDto);
            if (cart == null) return BadRequest();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDto>> RemoveCart(int id)
        {
            bool status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();

            return Ok(status);
        }
    }
}
