using Microsoft.AspNetCore.Http;
using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.IdentityDTO.Authentication;

namespace SEBO.Domain.Interface.Services.Identity
{
    public interface IAuthenticationService
    {
        Task<BaseResponseDTO<TokenDTO>> LoginByUserNameAsync(LoginRequestByUserNameDTO loginRequest);
        Task<BaseResponseDTO<TokenDTO>> RefreshLoginAsync(HttpContext request);
    }
}