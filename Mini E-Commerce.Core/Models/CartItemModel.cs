namespace Mini_E_Commerce.Core.Models
{
    public class CartItemModel 
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public CartModel Cart { get; set; }
        public ProductModel Product { get; set; }


    }
}
