using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Application.Dto.Order.Response
{
    public class ResponseGetOrderDetailsDto
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
