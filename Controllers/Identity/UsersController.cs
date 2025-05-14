
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;
using SEBO.API.Services.Identity;

namespace SEBO.API.Controllers.Identity
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ReadUserDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ReadUserDTO>>> RegisterAccount([FromBody] CreateUserDTO createUserDTO) => Ok(await _userService.RegisterAccountAsync(createUserDTO));

        [HttpPost("update")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ReadUserDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ReadUserDTO>>> UpdateUser([FromBody] UpdateUserDTO updateUserDTO) => Ok(await _userService.UpdateUser(updateUserDTO));

        [HttpGet("findAll")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<IEnumerable<ReadUserDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<IEnumerable<ReadUserDTO>>>> FindAll() => Ok(await _userService.FindAll());

        [HttpGet("me")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseResponseDTO<ReadUserDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<ReadUserDTO>>> GetCurrentUser() => Ok(await _userService.GetUser());
    }
}
