using Mini_E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Product.Response
{
    public class ResponseGetProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
       
    }
}
