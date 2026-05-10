using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Cart.Request
{
    public class RequestUpdateCartDto
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
