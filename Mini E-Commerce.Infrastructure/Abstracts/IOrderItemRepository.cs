using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface IOrderItemRepository : IGenericRepositoryAsync<OrderItemModel>
    {
        Task<List<OrderItemModel>> GetOrderItemsAsync(Guid orderId);
    }
}
