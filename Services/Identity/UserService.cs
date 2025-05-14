using System.Security.Claims;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.Utility.Exceptions;
using SEBO.API.Domain.ViewModel.DTO.Base;
using SEBO.API.Domain.ViewModel.DTO.IdentityDTO.Account;
using SEBO.API.Repository.IdentityAggregate;

namespace SEBO.API.Services.Identity
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseDTO<ReadUserDTO>> RegisterAccountAsync(CreateUserDTO createUserDto)
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

        public async Task<BaseResponseDTO<ReadUserDTO>> UpdateUser(UpdateUserDTO updateUserDTO)
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

        public async Task<BaseResponseDTO<IEnumerable<ReadUserDTO>>> FindAll()
        {
            var responseDTO = new BaseResponseDTO<IEnumerable<ReadUserDTO>>();

            var users = (await _userRepository.GetAllUsersAsync()).Select(x => new ReadUserDTO(x));

            return responseDTO.AddContent(users);
        }
        
        public async Task<BaseResponseDTO<ReadUserDTO>> GetUser()
        {
            var responseDTO = new BaseResponseDTO<ReadUserDTO>();
            var email = GetUserEmailFromClaims();
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new NotFoundException("User not found.");

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
