using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Product.Request;
using Mini_E_Commerce.Application.Dto.Product.Response;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Implementations
{
    public class ProductService : IProductService

    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IUserRepository userRepository,IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<ResponseAddProductDto>> AddAsync(Guid userId, RequestAddProductDto request)
        {
           
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<ResponseAddProductDto>.NotFound("User not found");
            }
            if(user.Role != "Admin")
            {
                return Result<ResponseAddProductDto>.BadRequest("You don't allow to add product");
            }
            if (request.StockQuantity <= 0)
            {
                return Result<ResponseAddProductDto>.BadRequest("Stock quantity must be more than 0");
            }
            if (request.Price <= 0)
            {
                return Result<ResponseAddProductDto>.BadRequest("Price must be more than 0");
            }
            var category=await _categoryRepository.GetByIdAsync(request.CategoryId);
            if(category == null)
            {
                return Result<ResponseAddProductDto>.NotFound("Category not found");
            }
            var name = request.Name.Trim();
            var description= request.Description.ToLower().Trim();
            var product = new ProductModel
            {
                Name = name,
                CategoryId = request.CategoryId,
                Description = description,
                Price = request.Price,
                StockQuantity = request.StockQuantity

            };
            await _productRepository.AddAsync(product);
            var ret = new ResponseAddProductDto
            {
                ProductId = product.Id
            };
            return Result<ResponseAddProductDto>.Success(ret, "Product added successfuly");
        }

        public async Task<Result<List<ResponseGetProductDto>>> GetAllAsync()
        {
            var products =  _productRepository.GetTableNoTracking().ToList();
            var ret = new List<ResponseGetProductDto>();
            if (products.Count == 0)
            {
                return Result<List<ResponseGetProductDto>>.Success(ret);
            }
            ret = products.Select(e => new ResponseGetProductDto
            {
                ProductId = e.Id,
                Name = e.Name,
                Description = e.Description,
                StockQuantity = e.StockQuantity,
                CategoryId = e.CategoryId,
                Price = e.Price

            }).ToList();
            return Result<List<ResponseGetProductDto>>.Success(ret);
        }

        public async Task<Result<ResponseGetProductDto>> GetAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return Result<ResponseGetProductDto>.NotFound("Product Not Found");
            }
            var ret = new ResponseGetProductDto
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                Price = product.Price
            };
            return Result<ResponseGetProductDto>.Success(ret);
        }

        public async Task<Result> SoftDeleteAsync(Guid userId, Guid productId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result.NotFound("User not found");
            }
            if (user.Role != "Admin")
            {
                return Result.BadRequest("You don't allow to delete product");
            }
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return Result.NotFound("Product Not Found");
            }
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;
            await _productRepository.SaveChangesAsync();
            return Result.Success("Product deleted successfuly");

        }

        public async Task<Result<ResponseGetProductDto>> UpdateAsync(Guid userId, Guid productId, RequestUpdateProductDto request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<ResponseGetProductDto>.NotFound("User not found");
            }
            if (user.Role != "Admin")
            {
                return Result<ResponseGetProductDto>.BadRequest("You don't allow to delete product");
            }
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return Result<ResponseGetProductDto>.NotFound("Product Not Found");
            }
            if (request.CategoryId.HasValue)
            {
                var category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
                if (category == null)
                {
                    return Result<ResponseGetProductDto>.NotFound("Category not found");
                }
                product.CategoryId = category.Id;
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                product.Name = request.Name.Trim();
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                product.Description = request.Description.ToLower().Trim();
            }
            if (request.StockQuantity.HasValue)
            {
                product.StockQuantity = request.StockQuantity.Value;
            }
            if (request.Price.HasValue)
            {
                product.Price = request.Price.Value;
            }
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.SaveChangesAsync();
            var ret = new ResponseGetProductDto
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                Price = product.Price
            };
            return Result<ResponseGetProductDto>.Success(ret);


        }
        
    }
}
