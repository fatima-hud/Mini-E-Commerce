using Azure.Core;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using MiniECommerce.Application.Abstracts;
using MiniECommerce.Application.Dto.Category.Request;
using MiniECommerce.Application.Dto.Category.Response;
using MiniECommerce.Core.Results;

namespace MiniECommerce.Application.Implementations
{
    public class CategoryService : ICategoryService
    {
       private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<ResponseAddCategoryDto>> AddAsync(Guid userId, RequestAddCategoryDto request)
        {
            var user=await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<ResponseAddCategoryDto>.NotFound("User not found.");
            }
            var isNameExist = await _categoryRepository.IsNameExistAsync(request.Name);
            if (isNameExist)
            {
                return Result<ResponseAddCategoryDto>.BadRequest("Category name already exists.");
            }
            if(user.Role != "Admin")
            {
                return Result<ResponseAddCategoryDto>.BadRequest("User not allow add category");
            }
            var category = new CategoryModel
            {
                Name = request.Name
            };
            await _categoryRepository.AddAsync(category);
            var ret = new ResponseAddCategoryDto
            {
                CategoryId = category.Id

            };
            return Result<ResponseAddCategoryDto>.Success(ret,"Category added successfuly");

        }

        public async Task<Result<List<ResponseGetCategoryDto>>> GetAllAsync()
        {
            var categories =  _categoryRepository.GetTableNoTracking().ToList();
            var ret= new List<ResponseGetCategoryDto>();
            if(categories.Count == 0)
            {
                return Result<List<ResponseGetCategoryDto>>.Success(ret);
            }
            ret = categories.Select(e => new ResponseGetCategoryDto
            {
                CategoryId = e.Id,
                Name = e.Name
            }).ToList();
              return Result<List<ResponseGetCategoryDto>>.Success(ret);


        }

        public async Task<Result<ResponseGetCategoryDto>> GetAsync(Guid categoryId)
        {
           var category=await _categoryRepository.GetByIdAsync(categoryId);
            if(category == null)
            {
                return Result<ResponseGetCategoryDto>.NotFound("Category not found.");
            }
            var ret = new ResponseGetCategoryDto
            {
                CategoryId = category.Id,
                Name = category.Name
            };
            return Result<ResponseGetCategoryDto>.Success(ret);
        }

        public async Task<Result> SoftDeleteAsync(Guid userId,Guid categoryId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result.NotFound("User not found.");
            }
          
            if (user.Role != "Admin")
            {
                return Result.BadRequest("User not allow add category");
            }
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return Result.NotFound("Category not found.");
            }

            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow;
            await _categoryRepository.SaveChangesAsync();
            return Result.Success("Deleted successfuly");



        }

        public async Task<Result<ResponseGetCategoryDto>> UpdateAsync(Guid userId, Guid categoryId, RequestAddCategoryDto request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<ResponseGetCategoryDto>.NotFound("User not found.");
            }
            if (user.Role != "Admin")
            {
                return Result<ResponseGetCategoryDto>.BadRequest("User not allow add category");
            }
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return Result<ResponseGetCategoryDto>.NotFound("Category not found.");
            }
            var isNameExist = await _categoryRepository.IsNameExistAsync(request.Name);
            if (isNameExist)
            {
                return Result<ResponseGetCategoryDto>.BadRequest("Catedory name is exist");
            }
            category.Name = request.Name;
            category.UpdatedAt= DateTime.UtcNow;
            await _categoryRepository.SaveChangesAsync();
            var ret = new ResponseGetCategoryDto
            {
                CategoryId = category.Id,
                Name = category.Name,
            };
            return Result<ResponseGetCategoryDto>.Success(ret,"Category updated successfuly");

        }
    }
}
