using Mini_E_Commerce.Core.Models;
using MiniECommerce.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Infrastructure.Abstracts
{
    public interface IProductRepository:IGenericRepositoryAsync<ProductModel>
    {
        Task<List<ProductModel>> GetSearchProductsAsync(string? search,Guid? categoryId,decimal? minPrice,decimal? maxPrice,int pageNumber,int pageSize);
    }
}
