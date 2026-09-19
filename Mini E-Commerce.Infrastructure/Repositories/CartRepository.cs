using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Context;
using MiniECommerce.Infrastructure.InfrastructureBases;

namespace MiniECommerce.Infrastructure.Repositories
{
    public class CartRepository : GenericRepositoryAsync<CartModel>, ICartRepository
    {
        public CartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartModel> GetCartByUserIdAsync(Guid customerId)
        {
            var res = await _dbContext.Carts.FirstOrDefaultAsync(e => e.CustomerId == customerId);
            return res;
        }

    }
}
