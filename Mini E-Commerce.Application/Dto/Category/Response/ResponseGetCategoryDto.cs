using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce.Application.Dto.Category.Response
{
    public class ResponseGetCategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } 
    }
}
