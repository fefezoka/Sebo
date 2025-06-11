using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.IdentityDTO.Account;

namespace SEBO.Domain.Interface.Services.Identity
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