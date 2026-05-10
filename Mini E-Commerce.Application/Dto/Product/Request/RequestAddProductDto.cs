using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mini_E_Commerce.Application.Dto.Product.Request
{
    public class RequestAddProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
