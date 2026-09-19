namespace MiniECommerce.Core.Models
{
    public class CategoryModel : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
