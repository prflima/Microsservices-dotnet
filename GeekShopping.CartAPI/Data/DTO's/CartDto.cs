namespace GeekShopping.CartAPI.Data;

public class CartDto
{
    public CartHeaderDto? CartHeader { get; set; }
    public IEnumerable<CartDetailDto>? CartDetails { get; set; }
}
