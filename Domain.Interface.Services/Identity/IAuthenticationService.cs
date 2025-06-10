using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Authentication;

namespace SEBO.API.Domain.Interface.Services.Identity
{
    public interface IAuthenticationService
    {
        Task<BaseResponseDTO<TokenDTO>> LoginByUserNameAsync(LoginRequestByUserNameDTO loginRequest);
        Task<BaseResponseDTO<TokenDTO>> RefreshLoginAsync(HttpContext request);
    }
}