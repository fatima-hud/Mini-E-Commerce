using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Product.Request;
using Mini_E_Commerce.Application.Dto.Product.Response;
using Mini_E_Commerce.Bases;
using Mini_E_Commerce.Extensions;
using MiniECommerce.Core.Results;

namespace Mini_E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ApiResult<ResponseAddProductDto>> AddAsync(Guid userId, RequestAddProductDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseAddProductDto>.BadRequest(errors);
            }
            var res = await _productService.AddAsync(userId, request);
            if (!res.IsSuccess)
            {
              return this.ToApiResult(res);
            }
            return ApiResult<ResponseAddProductDto>.Created(res.Value);
        }
        [HttpGet]
        public async Task<ApiResult<ResponseGetProductDto>> GetAsync(Guid productId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetProductDto>.BadRequest(errors);
            }
            var res = await _productService.GetAsync(productId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetProductDto>.Ok(res.Value);
        }
        [HttpGet]
        public async Task<ApiResult<List<ResponseGetProductDto>>> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetProductDto>>.BadRequest(errors);
            }
            var res = await _productService.GetAllAsync();
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetProductDto>>.Ok(res.Value);
        }
        [HttpPut]

        public async Task<ApiResult<ResponseGetProductDto>> UpdateAsync(Guid userId, Guid productId, RequestUpdateProductDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetProductDto>.BadRequest(errors);
            }
            var res = await _productService.UpdateAsync(userId,productId,request);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetProductDto>.Ok(res.Value);

        }
        [HttpDelete]
        public async Task<ApiResult> SoftDeleteAsync(Guid userId, Guid productId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult.BadRequest(errors);
            }
            var res=await _productService.SoftDeleteAsync(userId, productId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult.Ok("Deleted Successfuly");


        }
    }
}
