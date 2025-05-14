
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.Base;
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
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<IEnumerable<ItemDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<IEnumerable<ItemDTO>>>> GetItems() => Ok(await _itemService.GetItems());

        [HttpGet("{id:int}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ItemDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ItemDTO>>> GetItem([FromRoute] int id) => Ok(await _itemService.GetById(id));

        [HttpPost]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ItemDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ItemDTO>>> PostItem([FromBody] CreateItemDTO createItemDTO) => Ok(await _itemService.AddItem(createItemDTO));

        [HttpPut]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ItemDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ItemDTO>>> PutItem([FromBody] UpdateItemDTO updateItemDTO) => Ok(await _itemService.UpdateItem(updateItemDTO));

        [HttpDelete("{id:int}")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await _itemService.DeleteById(id);
            return NoContent();
        }
    }
}
