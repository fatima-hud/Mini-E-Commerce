using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface ICategoryRepository : IGenericRepositoryAsync<CategoryModel>
    {
        Task<bool> IsNameExistAsync(string name);
    }
}
