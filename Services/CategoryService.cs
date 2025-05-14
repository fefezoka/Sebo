using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.CategoryDTO;
using SEBO.API.Repository.ProductAggregate;

namespace SEBO.API.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
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

        public async Task<BaseResponseDTO<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<CategoryDTO>>();
            var categories = await _categoryRepository.GetAll() ?? Enumerable.Empty<Category>();

            return responseDTO.AddContent(categories.Select(x => new CategoryDTO(x)));
        }

        public async Task DeleteById(int id) => await _categoryRepository.DeleteById(id);
    }
}
