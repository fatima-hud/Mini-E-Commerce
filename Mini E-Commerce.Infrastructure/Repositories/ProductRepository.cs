using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<ProductModel>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductModel>> GetSearchProductsAsync(string? search, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize)
        {
            var products = _dbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice);
            }
            products = products.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            return await products.ToListAsync();


        }
    }
}
