
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Entities.ProductAggregate;
using SEBO.API.Domain.ViewModel.DTO.ItemDTO;
using SEBO.API.Services;

namespace SEBO.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems() => Ok(await _itemService.GetItems());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> GetItem([FromRoute]int id) => Ok(await _itemService.GetById(id));

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Item>> PostItem([FromBody] CreateItemDto createItemDto) => Ok(await _itemService.AddItem(createItemDto));

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<Item>> PutItem([FromBody] UpdateItemDto updateItemDto) => Ok(await _itemService.UpdateItem(updateItemDto));

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _itemService.DeleteById(id);
            return NoContent();
        }
    }
}
