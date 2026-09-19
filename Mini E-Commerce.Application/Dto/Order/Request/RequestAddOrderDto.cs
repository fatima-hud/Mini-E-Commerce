using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Application.Dto.Order.Request
{
    public class RequestAddOrderDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
