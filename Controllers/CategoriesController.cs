using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.ViewModel.DTO.CategoryDTO;
using SEBO.API.Services;

namespace SEBO.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetCategories() => Ok(await _categoryService.GetCategories());

        [HttpPost]
        public async Task<ActionResult<Item>> PostCategory([FromBody] CreateCategoryDto createCategoryDto) => Ok(await _categoryService.AddCategory(createCategoryDto));

        [HttpPut]
        public async Task<ActionResult<Item>> PutCategory([FromBody] UpdateCategoryDto updateCategoryDto) => Ok(await _categoryService.UpdateCategory(updateCategoryDto));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteById(id);
            return NoContent();
        }
    }
}
