
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Entities.IdentityAggregate;
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
        public async Task<ActionResult<IdentityResult>> RegisterAccount([FromBody] CreateUserDTO createUserDTO) => Ok(await _userService.RegisterAccountAsync(createUserDTO));

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<ReadUserDTO>> UpdateUser([FromBody] UpdateUserDTO updateUserDTO) =>  Ok(await _userService.UpdateUser(updateUserDTO));
       
        [HttpGet("findAll")]
        public async Task<ActionResult<IEnumerable<ReadUserDTO>>> FindAll() => Ok(await _userService.FindAll());

        [HttpGet("me")]
        public async Task<ActionResult<ReadUserDTO>> GetCurrentUser() => Ok(await _userService.GetUser());
    }
}
