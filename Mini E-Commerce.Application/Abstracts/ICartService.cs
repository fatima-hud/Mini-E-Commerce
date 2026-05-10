using Mini_E_Commerce.Application.Dto.Cart.Request;
using Mini_E_Commerce.Application.Dto.Cart.Response;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Abstracts
{
    public interface ICartService
    {
        Task<Result> AddAsync(Guid userId,RequestAddToCartDto request);
        Task<Result<ResponseGetCartItemDto>> UpdateAsync(Guid userId,RequestUpdateCartDto request);
        Task<Result> DeleteAsync(Guid userId,Guid cartItemId);
        Task<Result<List<ResponseGetCartDto>>> GetAsync(Guid userId);
        
    }
}
