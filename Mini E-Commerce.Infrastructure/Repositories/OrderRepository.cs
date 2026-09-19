using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Enums;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepositoryAsync<OrderModel>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<OrderModel>> GetAllByUserAsync(Guid userId)
        {
            var res = await _dbContext.Orders.Where(o => o.CustomerId == userId).ToListAsync();
            return res;
        }

        public async Task<List<OrderModel>> GetSearchOrdersAsync(Guid? customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate)
        {
            var orders = await _dbContext.Orders.ToListAsync();

            if (customerId.HasValue)
            {
                orders = orders.Where(o => o.CustomerId == customerId.Value).ToList();
            }

            if (status.HasValue)
            {
                orders = orders.Where(o => o.Status == status.Value).ToList();
            }

            if (fromDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt >= fromDate.Value).ToList();
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.CreatedAt <= endDate.Value).ToList();
            }

            return orders;
        }


    }
}
