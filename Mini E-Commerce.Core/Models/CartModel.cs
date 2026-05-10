using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
{
    public class CartModel:BaseEntity
    {
        public Guid CustomerId { get; set; }
        public UserModel Customer { get; set; }
        public ICollection<CartItemModel>? CartItems { get; set; }

    }
}
