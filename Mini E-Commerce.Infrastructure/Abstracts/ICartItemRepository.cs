
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface ICartItemRepository : IGenericRepositoryAsync<CartItemModel>
    {
        Task<List<CartItemModel>> GetAllAsync(Guid cartId);
        Task<CartItemModel> CheckItemExistAsync(Guid cartId, Guid productId);
    }
}
