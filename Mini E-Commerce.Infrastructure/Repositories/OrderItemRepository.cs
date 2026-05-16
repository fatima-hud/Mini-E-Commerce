using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Repositories
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
