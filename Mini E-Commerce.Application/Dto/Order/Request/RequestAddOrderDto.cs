using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Order.Request
{
    public class RequestAddOrderDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
