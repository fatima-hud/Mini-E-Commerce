using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface ICartRepository : IGenericRepositoryAsync<CartModel>
    {
        Task<CartModel> GetCartByUserIdAsync(Guid customerId);
    }
}
