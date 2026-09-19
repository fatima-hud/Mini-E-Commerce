
using MiniECommerce.Core.Enums;

namespace MiniECommerce.Core.Models
{
    public class OrderModel:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public UserModel Customer { get; set; }
   
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } 
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }
}
