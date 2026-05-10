using System.ComponentModel.DataAnnotations;

namespace MiniECommerce.Application.Dto.Category.Request
{
    public class RequestAddCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
