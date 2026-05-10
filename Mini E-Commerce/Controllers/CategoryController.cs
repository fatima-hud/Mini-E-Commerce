using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce.Application.Dto.Product.Response;
using Mini_E_Commerce.Bases;
using Mini_E_Commerce.Extensions;
using MiniECommerce.Application.Abstracts;
using MiniECommerce.Application.Dto.Category.Request;
using MiniECommerce.Application.Dto.Category.Response;
using MiniECommerce.Core.Results;

namespace Mini_E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ApiResult<ResponseAddCategoryDto>> AddAsync(Guid userId, RequestAddCategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseAddCategoryDto>.BadRequest(errors);
            }
            var res = await _categoryService.AddAsync(userId, request);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseAddCategoryDto>.Created(res.Value);
        }
        [HttpGet]
        public async Task<ApiResult<ResponseGetCategoryDto>> GetAsync(Guid categoryId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetCategoryDto>.BadRequest(errors);
            }
            var res = await _categoryService.GetAsync(categoryId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetCategoryDto>.Ok(res.Value);

        }
        [HttpGet]
        public async Task<ApiResult<List<ResponseGetCategoryDto>>> GetAllAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetCategoryDto>>.BadRequest(errors);
            }

            var res = await _categoryService.GetAllAsync();
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetCategoryDto>>.Ok(res.Value);
        }
        [HttpPut]
        public async Task<ApiResult<ResponseGetCategoryDto>> UpdateAsync(Guid userId, Guid categoryId, RequestAddCategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetCategoryDto>.BadRequest(errors);
            }

            var res = await _categoryService.UpdateAsync(userId, categoryId, request);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetCategoryDto>.Ok(res.Value);
        }
        [HttpDelete]
        public async Task<ApiResult> SoftDeleteAsync(Guid userId, Guid categoryId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult.BadRequest(errors);
            }
            var res = await _categoryService.SoftDeleteAsync(userId, categoryId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult.Ok();

        }

    }
}