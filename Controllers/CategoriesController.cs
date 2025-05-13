using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<Item>> PostCategory([FromBody] CreateCategoryDTO createCategoryDTO) => Ok(await _categoryService.AddCategory(createCategoryDTO));

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Item>> PutCategory([FromBody] UpdateCategoryDTO updateCategoryDTO) => Ok(await _categoryService.UpdateCategory(updateCategoryDTO));

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            await _categoryService.DeleteById(id);
            return NoContent();
        }
    }
}
