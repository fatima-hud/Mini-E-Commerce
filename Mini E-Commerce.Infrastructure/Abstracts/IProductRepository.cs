using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface IProductRepository : IGenericRepositoryAsync<ProductModel>
    {
        Task<List<ProductModel>> GetSearchProductsAsync(string search, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize);
    }
}
