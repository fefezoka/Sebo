using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SEBO.API.Domain.Interface.Services.Identity;
using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;

namespace SEBO.API.Controllers.Identity
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseResponseDTO<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(BaseResponseDTO<string>))]
        public async Task<ActionResult<BaseResponseDTO<TokenDTO>>> LoginByUserName([FromBody] LoginRequestByUserNameDTO loginRequestDTO)
        {
            var loginResult = await _authenticationService.LoginByUserNameAsync(loginRequestDTO);
            return loginResult.IsSuccess ? Ok(loginResult) : Unauthorized(loginResult);
        }
    }
}
