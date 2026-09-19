using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepositoryAsync<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            var res = await _dbContext.Categories.AnyAsync(e => e.Name == name && !e.IsDeleted);
            return res;

        }
    }
}
