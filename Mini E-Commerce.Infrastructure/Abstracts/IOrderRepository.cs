using MiniECommerce.Core.Enums;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Abstracts
{
    public interface IOrderRepository : IGenericRepositoryAsync<OrderModel>
    {
        Task<List<OrderModel>> GetAllByUserAsync(Guid userId);
        Task<List<OrderModel>> GetSearchOrdersAsync(Guid? customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate);
    }
}
