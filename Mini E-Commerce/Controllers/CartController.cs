using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Cart.Request;
using Mini_E_Commerce.Application.Dto.Cart.Response;
using Mini_E_Commerce.Bases;
using Mini_E_Commerce.Extensions;
using MiniECommerce.Application.Dto.Category.Response;
using MiniECommerce.Core.Results;
using System.Reflection.Metadata.Ecma335;

namespace Mini_E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<ApiResult> AddAsync(Guid userId, RequestAddToCartDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult.BadRequest(errors);
            }
            var res = await _cartService.AddAsync(userId, request);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult.Created(res.Message);
        }
        [HttpGet]
        public async Task<ApiResult<List<ResponseGetCartDto>>> GetAsync(Guid userId)
        {

            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetCartDto>>.BadRequest(errors);
            }
            var res = await _cartService.GetAsync(userId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetCartDto>>.Ok(res.Value);
        }
        [HttpPut]
        public async Task<ApiResult<ResponseGetCartItemDto>> UpdateAsync(Guid userId, RequestUpdateCartDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetCartItemDto>.BadRequest(errors);
            }
            var res = await _cartService.UpdateAsync(userId, request);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetCartItemDto>.Ok(res.Value);
        }
        [HttpDelete]
        public async Task<ApiResult> DeleteAsync(Guid userId, Guid cartItemId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult.BadRequest(errors);
            }
            var res = await _cartService.DeleteAsync(userId, cartItemId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult.Ok(res.Message);


        }
      

    }
}
