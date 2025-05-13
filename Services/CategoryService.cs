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

        public async Task<Category> AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            var category = new Category()
            {
               Description = createCategoryDTO.Description,
               Name = createCategoryDTO.Name,
            };

            return await _categoryRepository.Add(category);
        }

        public async Task<Category> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _categoryRepository.GetById(updateCategoryDTO.CategoryId);

            if (category == null) throw new NotFoundException("Category not found");

            category.Description = updateCategoryDTO.Description;
            category.Name = updateCategoryDTO.Name;

            return await _categoryRepository.Update(category);
        }

        public async Task<IEnumerable<Category>> GetCategories() => await _categoryRepository.GetAll() ?? Enumerable.Empty<Category>();

        public async Task DeleteById(int id) => await _categoryRepository.DeleteById(id);
    }
}
