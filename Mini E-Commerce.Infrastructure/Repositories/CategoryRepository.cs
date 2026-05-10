using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            var res=await _dbContext.Categories.AnyAsync(e=>e.Name==name &&!e.IsDeleted);
            return res;

        }
    }
}
