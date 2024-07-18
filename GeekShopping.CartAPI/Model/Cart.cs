using GeekShopping.CartAPI.Model.Base;

namespace GeekShopping.CartAPI.Model
{
    public class Cart : BaseEntity
    {
        public CartHeader? CartHeader { get; set; }
        public IEnumerable<CartDetail>? CartDetails { get; set; }
    }
}
