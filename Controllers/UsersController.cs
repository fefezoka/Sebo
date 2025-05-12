
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO;
using SEBO.API.Services;
using SEBO.API.Services.AppServices.IdentityService;
using SEBO.Domain.ViewModel.DTO.IdentityDTO;

namespace SEBO.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authenticationService;

        public UsersController(UserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
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

        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> LoginByUserName([FromBody] LoginRequestByUserNameDTO loginRequestDTO)
        {
            var loginResult = await _authenticationService.LoginByUserNameAsync(loginRequestDTO);
            return loginResult.IsSuccess ? Ok(loginResult) : Unauthorized(loginResult);
        }

        [HttpGet("teste")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> getUSER()
        {
            return await _userService.GetUser();
        }
    }
}
