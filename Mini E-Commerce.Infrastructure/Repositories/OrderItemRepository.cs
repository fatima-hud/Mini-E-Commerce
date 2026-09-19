using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepositoryAsync<OrderItemModel>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<OrderItemModel>> GetOrderItemsAsync(Guid orderId)
        {
            var res = await _dbContext.OrderItems.Where(e => e.OrderId == orderId).ToListAsync();
            return res;
        }
    }
}
