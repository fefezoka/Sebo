using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.CategoryDTO;

namespace SEBO.Domain.Interface.Services
{
    public interface ICategoryService
    {
        Task<BaseResponseDTO<CategoryDTO>> AddCategory(CreateCategoryDTO createCategoryDTO);
        Task<BaseResponseDTO<CategoryDTO>> UpdateCategory(UpdateCategoryDTO updateCategoryDTO);
        Task<BaseResponseDTO<IEnumerable<CategoryDTO>>> GetAllCategories();
        Task DeleteCategoryById(int id);
    }
}
