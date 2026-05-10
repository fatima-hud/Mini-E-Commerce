using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Product.Request
{
    public class RequestUpdateProductDto
    {
      
        public string? Name { get; set; }
        
        public string? Description { get; set; }
       
        public int? StockQuantity { get; set; }
      
        public decimal? Price { get; set; }
        
        public Guid? CategoryId { get; set; }
    }
}
