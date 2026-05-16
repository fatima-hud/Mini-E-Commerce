using Mini_E_Commerce.Core.Enums;
using Mini_E_Commerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace Mini_E_Commerce.Infrastructure.Abstracts
{
    public interface IOrderRepository : IGenericRepositoryAsync<OrderModel>
    {
        Task<List<OrderModel>> GetAllByUserAsync(Guid userId);
        Task<List<OrderModel>> GetSearchOrdersAsync( Guid? customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate);
    }
}
