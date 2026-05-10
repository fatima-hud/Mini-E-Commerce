using Mini_E_Commerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Abstracts
{
    public interface ICartItemRepository:IGenericRepositoryAsync<CartItemModel>
    {
        Task<List<CartItemModel>> GetAllAsync(Guid cartId);
        Task<CartItemModel> CheckItemExistAsync(Guid cartId,Guid productId);
    }
}
