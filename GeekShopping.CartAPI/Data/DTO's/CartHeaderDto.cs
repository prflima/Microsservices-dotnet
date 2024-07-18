namespace GeekShopping.CartAPI.Data;

public class CartHeaderDto
{
    public long Id { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
}
