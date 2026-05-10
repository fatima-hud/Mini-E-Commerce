using Mini_E_Commerce.Application.Dto.Product.Request;
using Mini_E_Commerce.Application.Dto.Product.Response;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Abstracts
{
    public interface IProductService
    {
        Task<Result<ResponseAddProductDto>> AddAsync(Guid userId,RequestAddProductDto request);
        Task<Result<ResponseGetProductDto>> UpdateAsync(Guid userId,Guid productId,RequestUpdateProductDto request);
        Task<Result> SoftDeleteAsync(Guid userId,Guid productId);
        Task<Result<ResponseGetProductDto>> GetAsync(Guid productId);
        Task<Result<List<ResponseGetProductDto>>> GetAllAsync();
    }
}
    