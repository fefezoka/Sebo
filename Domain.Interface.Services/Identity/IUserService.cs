using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;

namespace SEBO.API.Domain.Interface.Services.Identity
{
    public interface IUserService
    {
        Task<BaseResponseDTO<ReadUserDTO>> RegisterUserAsync(CreateUserDTO userDTO);
        Task<BaseResponseDTO<ReadUserDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        Task<BaseResponseDTO<IEnumerable<ReadUserDTO>>> GetAllUsersAsync();
        Task<BaseResponseDTO<ReadUserDTO>> GetActiveUserAsync();
        string GetUserEmailFromClaims();
        int GetUserIdFromClaims();
    }
}