using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;
using System.Security.Claims;

namespace SEBO.API.Services.Identity
{
    public class AuthenticationService
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, TokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<BaseResponseDTO<TokenDTO>> LoginByUserNameAsync(LoginRequestByUserNameDTO loginRequest)
        {
            try
            {
                var applicationUser = await _signInManager.UserManager.FindByNameAsync(loginRequest.UserName);
                if (applicationUser is null) return new BaseResponseDTO<TokenDTO>().WithErrors(GetErrors());

                return await LoginAsync(applicationUser, loginRequest.Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponseDTO<TokenDTO>> RefreshLoginAsync(HttpContext request)
        {
            try
            {
                var responseDTO = new BaseResponseDTO<TokenDTO>();
                var claims = request.User.Identity as ClaimsIdentity;
                var userId = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _signInManager.UserManager.FindByIdAsync(userId);
                if (user is null) return new BaseResponseDTO<TokenDTO>().WithErrors(GetErrors());

                if (await _signInManager.UserManager.IsLockedOutAsync(user))
                {
                    return new BaseResponseDTO<TokenDTO>().WithErrors(GetErrors());
                }

                return responseDTO.AddContent(await GenerateToken(user));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<BaseResponseDTO<TokenDTO>> LoginAsync(ApplicationUser applicationUser, string password)
        {
            try
            {
                var LoginResponseDTO = new BaseResponseDTO<TokenDTO>();
                var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(applicationUser, password, false);

                if (checkPasswordResult.Succeeded)
                {
                    return LoginResponseDTO.AddContent(await GenerateToken(applicationUser));
                }

                return LoginResponseDTO.WithErrors(GetErrors(checkPasswordResult));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<TokenDTO> GenerateToken(ApplicationUser applicationUser)
        {
            var userRoles = await _signInManager.UserManager.GetRolesAsync(applicationUser);
            var userClaims = await _signInManager.UserManager.GetClaimsAsync(applicationUser);
            var token = await _tokenService.GetToken(applicationUser, userClaims, userRoles);
            return new TokenDTO(token.Value);
        }

        private IEnumerable<string> GetErrors(SignInResult? result = null)
        {
            var errorList = new List<string>();

            if (result is not null)
            {
                if (result.IsLockedOut) errorList.Add("LockedOut");
                if (result.IsNotAllowed) errorList.Add("NotAllowed");
                if (result.RequiresTwoFactor) errorList.Add("RequiresTwoFactor");
            }

            if (errorList.IsNullOrEmpty())
                errorList.Add("User or password are incorrect");

            return errorList;
        }
    }
}