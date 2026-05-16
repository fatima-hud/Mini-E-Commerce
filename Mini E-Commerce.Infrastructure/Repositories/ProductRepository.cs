using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<ProductModel>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ProductModel>> GetSearchProductsAsync(string? search, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize)
        {
            var products=await _dbContext.Products.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                products=products.Where(p => p.Name.Contains(search) || p.Description.Contains(search)).ToList();
            }
            if (categoryId != null)
            {
                products=products.Where(p => p.CategoryId == categoryId).ToList();
            }
            if (minPrice != null)
            {
                products=products.Where(p => p.Price >= minPrice).ToList();
            }
            if (maxPrice != null)
            {
                products=products.Where(p => p.Price <= maxPrice).ToList();
            }
            products=products.Skip(pageNumber*(pageSize-1)).Take(pageSize).ToList();
            return products;
           
           
        }
    }
}
