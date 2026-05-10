using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
{
    public class ProductModel:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public ICollection<CartItemModel>? CartItems { get; set; }
        public ICollection<OrderItemModel>? OrderItems { get; set; }


    }
}
