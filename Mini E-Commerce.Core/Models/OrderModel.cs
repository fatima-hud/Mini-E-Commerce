using Mini_E_Commerce.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
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
