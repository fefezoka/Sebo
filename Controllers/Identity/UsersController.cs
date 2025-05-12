
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO;
using SEBO.API.Services.Identity;
using SEBO.Domain.ViewModel.DTO.IdentityDTO;

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
        public async Task<ActionResult<IdentityResult>> RegisterAccount([FromBody] CreateUserDTO createUserRequest)
        {
            return await _userService.RegisterAccountAsync(createUserRequest);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<IdentityResult>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            return await _userService.UpdateUser(updateUserDto);
        }
    }
}
