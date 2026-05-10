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
    }
}
