namespace MiniECommerce.Core.Models
{
    public class CartModel : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public UserModel Customer { get; set; }
        public ICollection<CartItemModel>? CartItems { get; set; }

    }
}
