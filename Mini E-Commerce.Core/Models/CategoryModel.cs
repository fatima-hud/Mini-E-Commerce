using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
{
    public class CategoryModel:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
