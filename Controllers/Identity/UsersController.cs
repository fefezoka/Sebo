
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadUserDTO>> RegisterAccount([FromBody] CreateUserDTO createUserDTO) => Ok(await _userService.RegisterAccountAsync(createUserDTO));

        [HttpPost("update")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadUserDTO>> UpdateUser([FromBody] UpdateUserDTO updateUserDTO) =>  Ok(await _userService.UpdateUser(updateUserDTO));
       
        [HttpGet("findAll")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReadUserDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> FindAll() => Ok(await _userService.FindAll());
        
        [HttpGet("me")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadUserDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadUserDTO>> GetCurrentUser() => Ok(await _userService.GetUser());
    }
}
