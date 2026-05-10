using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Cart.Request
{
    public class RequestAddToCartDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
