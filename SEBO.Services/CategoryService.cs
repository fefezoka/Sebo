using SEBO.Domain.Entities.ProductAggregate;
using SEBO.Domain.Interface.Services;
using SEBO.Domain.Interface.Repository.ProductAggregate;
using SEBO.Domain.Utility.Exceptions;
using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.CategoryDTO;

namespace SEBO.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponseDTO<CategoryDTO>> AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            var responseDTO = new BaseResponseDTO<CategoryDTO>();
            var category = new Category()
            {
                Description = createCategoryDTO.Description,
                Name = createCategoryDTO.Name,
            };

            category = await _categoryRepository.Add(category);

            return responseDTO.AddContent(new CategoryDTO(category));
        }

        public async Task<BaseResponseDTO<CategoryDTO>> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            var responseDTO = new BaseResponseDTO<CategoryDTO>();
            var category = await _categoryRepository.GetById(updateCategoryDTO.CategoryId);

            if (category == null) throw new NotFoundException("Category not found");

            category.Description = updateCategoryDTO.Description;
            category.Name = updateCategoryDTO.Name;


            category = await _categoryRepository.Update(category);

            return responseDTO.AddContent(new CategoryDTO(category));
        }

        public async Task<BaseResponseDTO<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<CategoryDTO>>();
            var categories = await _categoryRepository.GetAll() ?? Enumerable.Empty<Category>();

            return responseDTO.AddContent(categories.Select(x => new CategoryDTO(x)));
        }

        public async Task DeleteCategoryById(int id) => await _categoryRepository.DeleteById(id);
    }
}
