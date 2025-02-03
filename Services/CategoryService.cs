using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.CategoryDTO;
using SEBO.API.Domain.ViewModel.DTO.ItemDTO;
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

        public async Task<Category> AddCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category()
            {
               Description = createCategoryDto.Description,
               Name = createCategoryDto.Name,
            };

            return await _categoryRepository.Add(category);
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDto updateCategory)
        {
            var category = await _categoryRepository.GetById(updateCategory.CategoryId);

            if (category == null) throw new NotFoundException("Category not found");

            category.Description = updateCategory.Description;
            category.Name = updateCategory.Name;

            return await _categoryRepository.Update(category);
        }

        public async Task<IEnumerable<Category>> GetCategories() => await _categoryRepository.GetAll() ?? Enumerable.Empty<Category>();

        public async Task DeleteById(int id) => await _categoryRepository.DeleteById(id);
    }
}
