using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Models;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.InfrastructureBases;


namespace MiniECommerce.Infrastructure.Repositories
{
    public class CartItemRepository : GenericRepositoryAsync<CartItemModel>, ICartItemRepository
    {
        public CartItemRepository(Context.ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<CartItemModel> CheckItemExistAsync(Guid cartId, Guid productId)
        {
            var res = await _dbContext.CartItems.Include(e => e.Cart).FirstOrDefaultAsync(e => e.CartId == cartId && e.ProductId == productId);
            return res;
        }

        public async Task<List<CartItemModel>> GetAllAsync(Guid cartId)
        {
            var res = await _dbContext.CartItems.Include(e => e.Product).Where(e => e.CartId == cartId)
                 .ToListAsync();
            return res;
        }
    }
}
