using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;
using SEBO.API.Services.Identity;

namespace SEBO.API.Controllers.Identity
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDTO>> LoginByUserName([FromBody] LoginRequestByUserNameDTO loginRequestDTO, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            var loginResult = await _authenticationService.LoginByUserNameAsync(loginRequestDTO, useCookies, useSessionCookies);
            return loginResult.IsSuccess ? Ok(loginResult) : Unauthorized(loginResult);
        }
    }
}
