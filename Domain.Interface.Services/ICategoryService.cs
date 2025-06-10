using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.CategoryDTO;

namespace SEBO.API.Domain.Interface.Services
{
    public interface ICategoryService
    {
        Task<BaseResponseDTO<CategoryDTO>> AddCategory(CreateCategoryDTO createCategoryDTO);
        Task<BaseResponseDTO<CategoryDTO>> UpdateCategory(UpdateCategoryDTO updateCategoryDTO);
        Task<BaseResponseDTO<IEnumerable<CategoryDTO>>> GetAllCategories();
        Task DeleteCategoryById(int id);
    }
}
