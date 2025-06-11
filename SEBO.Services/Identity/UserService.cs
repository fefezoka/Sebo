using System.Security.Claims;
using SEBO.Domain.Entities.IdentityAggregate;
using SEBO.Domain.Interface.Services.Identity;
using SEBO.Domain.Utility.Exceptions;
using SEBO.Domain.Dto.DTO.Base;
using SEBO.Domain.Dto.DTO.IdentityDTO.Account;
using SEBO.Domain.Interface.Repository.IdentityAggregate;
using Microsoft.AspNetCore.Http;

namespace SEBO.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseDTO<ReadUserDTO>> RegisterUserAsync(CreateUserDTO createUserDto)
        {
            var responseDTO = new BaseResponseDTO<ReadUserDTO>();

            var applicationUser = new ApplicationUser()
            {
                UserName = createUserDto.UserName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
            };

            var (result, user) = await _userRepository.AddUserAsync(applicationUser, createUserDto.Password);

            return responseDTO.AddContent(new ReadUserDTO(user));
        }

        public async Task<BaseResponseDTO<ReadUserDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            var responseDTO = new BaseResponseDTO<ReadUserDTO>();
            var userId = GetUserIdFromClaims();
            var applicationUser = new ApplicationUser()
            {
                UserName = updateUserDTO.UserName,
                LastName = updateUserDTO.LastName,
                Email = updateUserDTO.Email,
                FirstName = updateUserDTO.FirstName,
            };

            var (result, user) = await _userRepository.UpdateUserAsync(userId, applicationUser);

            return responseDTO.AddContent(new ReadUserDTO(user));
        }

        public async Task<BaseResponseDTO<IEnumerable<ReadUserDTO>>> GetAllUsersAsync()
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<ReadUserDTO>>();

            var users = (await _userRepository.GetAllUsersAsync()).Select(x => new ReadUserDTO(x));

            return responseDTO.AddContent(users);
        }

        public async Task<BaseResponseDTO<ReadUserDTO>> GetActiveUserAsync()
        {
            var responseDTO = new BaseResponseDTO<ReadUserDTO>();
            var email = GetUserEmailFromClaims();
            var (result, user) = await _userRepository.GetUserByEmailAsync(email);

            if (user == null) throw new NotFoundException("User not found");

            return responseDTO.AddContent(new ReadUserDTO(user));
        }

        public async Task<BaseResponseDTO<ReadUserDTO>> GetUserById(int id)
        {
            var responseDTO = new BaseResponseDTO<ReadUserDTO>();
            var (result, user) = await _userRepository.GetUserByIdAsync(id);

            if (user == null) throw new NotFoundException("User not found");

            return responseDTO.AddContent(new ReadUserDTO(user));
        }

        public string GetUserEmailFromClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
        }

        public int GetUserIdFromClaims()
        {
            return int.Parse(_httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value);
        }
    }
}
