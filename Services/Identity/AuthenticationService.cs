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
        private readonly IMapper _mapper;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager, TokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<BaseResponseDTO<TokenDTO>> LoginByUserNameAsync(LoginRequestByUserNameDTO loginRequestDTO, bool? useCookies = false, bool? useSessionCookies = false)
        {
            try
            {
                var applicationUser = await _signInManager.UserManager.FindByNameAsync(loginRequestDTO.UserName);
                if (applicationUser is null) return new BaseResponseDTO<TokenDTO>().WithErrors(GetErrors());

                var LoginResponseDTO = new BaseResponseDTO<TokenDTO>();

                var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
                var isPersistent = (useCookies == true) && (useSessionCookies != true);
                _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
                var result = await _signInManager.PasswordSignInAsync(loginRequestDTO.UserName, loginRequestDTO.Password, isPersistent, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return LoginResponseDTO.AddContent(await GenerateToken(applicationUser));
                }

                return LoginResponseDTO.WithErrors(GetErrors(result));
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

        private async Task<TokenDTO> GenerateToken(ApplicationUser applicationUser)
        {
            var userRoles = await _signInManager.UserManager.GetRolesAsync(applicationUser);
            var userClaims = await _signInManager.UserManager.GetClaimsAsync(applicationUser);
            var token = await _tokenService.GetToken(applicationUser, userClaims, userRoles);
            var tokenResponseDTO = _mapper.Map<TokenDTO>(token.Value);
            return tokenResponseDTO;
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