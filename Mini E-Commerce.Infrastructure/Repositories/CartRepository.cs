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
    public class CartRepository : GenericRepositoryAsync<CartModel>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartModel> GetCartByUserIdAsync(Guid customerId)
        {
            var res=await _dbContext.Carts.FirstOrDefaultAsync(e=>e.CustomerId==customerId);
            return res;
        }

    }
}
