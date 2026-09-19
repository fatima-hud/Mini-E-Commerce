using MiniECommerce.Core.Enums;
using MiniECommerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Application.Dto.Order.Response
{
    public class ResponseGetOrderDto
    {
        public Guid OrderId {  get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
