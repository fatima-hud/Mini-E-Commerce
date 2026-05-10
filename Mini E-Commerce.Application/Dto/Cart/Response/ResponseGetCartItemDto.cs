using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Cart.Response
{
    public class ResponseGetCartItemDto
    {
        public Guid CartItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
         public decimal Price { get; set; }

    }
}
