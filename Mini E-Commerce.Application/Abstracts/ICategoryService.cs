
using MiniECommerce.Application.Dto.Category.Request;
using MiniECommerce.Application.Dto.Category.Response;
using MiniECommerce.Core.Results;

namespace MiniECommerce.Application.Abstracts
{
    public interface ICategoryService
    {
        Task<Result<ResponseAddCategoryDto>> AddAsync(Guid userId, RequestAddCategoryDto request);
        Task<Result<ResponseGetCategoryDto>> UpdateAsync(Guid userId, Guid categoryId, RequestAddCategoryDto request);
        Task<Result<ResponseGetCategoryDto>> GetAsync(Guid categoryId);
        Task<Result<List<ResponseGetCategoryDto>> >GetAllAsync();
        Task<Result> SoftDeleteAsync(Guid userId,Guid categoryId);
    }
}
